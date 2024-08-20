using NBitcoin;
using SecureDeck.Helpers;
using SecureDeck.Interfaces;
using SecureDeck.Models;
using System.Security.Cryptography;
using System.Text;

namespace SecureDeck.Services
{
    public class DeckService : IDeckService
    {
        public string DeckPublicBitcoinAddress => _bitcoinAddress;
        private string _deckString = "";
        private string _bitcoinAddress = "";
        private readonly Deck _deck;
        private readonly List<Card> _usedCards;
        private readonly CryptoHelper _cryptoHelper;

        // Private global variables
        private List<ShuffleParticipant> _shuffleParticipants;

        public DeckService()
        {
            _deck = new Deck();
            _usedCards = [];
            _cryptoHelper = new CryptoHelper();
            _shuffleParticipants = [];
        }

        // Method to create a standard 52-card deck
        private void InitializeDeck()
        {
            var cards = new List<Card>();

            foreach (SuitType suit in Enum.GetValues(typeof(SuitType)))
            {
                foreach (RankType rank in Enum.GetValues(typeof(RankType)))
                {
                    cards.Add(new Card(rank, suit));
                }
            }

            _deck.InitializeDeck(cards);
        }

        // Method to incorporate player entropy and shuffle the deck
        public void ShuffleDeck(List<ShuffleParticipant> shuffleParticipants)
        {
            InitializeDeck();

            // Store shuffle participants (players and operator)
            _shuffleParticipants = shuffleParticipants;

            // Combine all seeds into one final seed
            var combinedSeed = _cryptoHelper.CombineSeeds(_shuffleParticipants);

            // Use the combined seed to shuffle the deck
            ShuffleWithSeed(combinedSeed);

            // After shuffling, represent the deck as a Bitcoin address
            RepresentDeckAsBitcoinAddress(combinedSeed);
        }

        // Shuffle the deck using the combined seed
        private void ShuffleWithSeed(byte[] seed)
        {
            var rng = new Random(BitConverter.ToInt32(seed, 0));
            var shuffledCards = _deck.Cards.OrderBy(card => rng.Next()).ToList();
            _deck.Cards.Clear();
            _deck.Cards.AddRange(shuffledCards);
        }

        private void RepresentDeckAsBitcoinAddress(byte[] combinedSeed)
        {
            // Serialize the deck into a string
            _deckString = DeckAsString();

            // Get the hash of the deck
            var deckHash = _cryptoHelper.GetHashString(_deckString, combinedSeed);

            // Combine the deck string with the combined seed
            string combinedString = deckHash + BitConverter.ToString(combinedSeed);

            // Use NBitcoin to generate a valid private key
            var privateKeyBytes = SHA256.HashData(Encoding.UTF8.GetBytes(combinedString));
            var privateKey = new Key(privateKeyBytes);

            // Derive the public key from the private key
            _bitcoinAddress = privateKey.PubKey.GetAddress(ScriptPubKeyType.Legacy, Network.Main).ToString();
        }

        // Convert deck to string
        private string DeckAsString()
        {
            return string.Join(" ", _deck.Cards.Select(card => card.ToShortString()));
        }

        // Method to deal a specific number of cards from the top of the deck
        public List<Card> Deal(int numberOfCards)
        {
            if (numberOfCards > _deck.Cards.Count)
            {
                throw new ArgumentException("Not enough cards in the deck to deal.");
            }

            List<Card> dealtCards = _deck.Cards.Take(numberOfCards).ToList();
            _usedCards.AddRange(dealtCards);
            _deck.Cards.RemoveRange(0, numberOfCards);
            return dealtCards;
        }

        // Method to get the complete deck information including the BTC keys
        public ShuffledDeck GetFinalPostShuffleDeckState()
        {
            return new ShuffledDeck()
            {
                DeckString = _deckString,
                Participants = _shuffleParticipants,
                BitcoinAddress = _bitcoinAddress
            };
        }
    }
}
