using DeckValidationService.Interfaces;
using NBitcoin;
using SecureDeck.Helpers;
using SecureDeck.Models;
using System.Security.Cryptography;
using System.Text;

namespace DeckValidationServcie.Services
{
    public class DeckValidator : IDeckValidator
    {
        private readonly CryptoHelper _cryptoHelper;

        public DeckValidator()
        {
            _cryptoHelper = new CryptoHelper();
        }

        public ValidateDeckResult ValidateDeck(ShuffledDeck request)
        {
            // Check if participants list is empty and throw an exception if it is
            if (request.Participants == null || !request.Participants.Any())
            {
                throw new ArgumentException("Participants cannot be empty.");
            }

            // Combine the player seeds into a single seed
            var combinedSeed = _cryptoHelper.CombineSeeds(request.Participants);

            // Serialize the deck into a string
            var deckHash = _cryptoHelper.GetHashString(request.DeckString, combinedSeed);

            // Combine the deck string with the combined seed
            string combinedString = deckHash + BitConverter.ToString(combinedSeed);

            // Generate the private key from the combined string
            var privateKeyBytes = SHA256.HashData(Encoding.UTF8.GetBytes(combinedString));
            var privateKey = new Key(privateKeyBytes);

            // Derive the public key and Bitcoin address from the private key
            var bitcoinAddress = privateKey.PubKey.GetAddress(ScriptPubKeyType.Legacy, Network.Main).ToString();

            // Validate that the generated Bitcoin address matches the one in the request
            var result = new ValidateDeckResult
            {
                Request = request,
                ResultingBitcoinAddress = bitcoinAddress,
                IsValid = request.BitcoinAddress == bitcoinAddress,
                ValidatedDate = DateTime.UtcNow,
            };

            return result;
        }


    }
}
