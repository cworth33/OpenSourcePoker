using SecureDeck.Models;
using SecureDeck.Services;

namespace SecureDeck.Tests
{
    [TestFixture]
    public class DeckServiceTests
    {
        private DeckService _deckService;
        private List<ShuffleParticipant> _participants;

        [SetUp]
        public void SetUp()
        {
            _deckService = new DeckService();
            _participants = new List<ShuffleParticipant>
            {
                new ShuffleParticipant { Name = "Player1", Seed = "seed1" },
                new ShuffleParticipant { Name = "Player2", Seed = "seed2" }
            };
        }

        [Test]
        public void DeckService_Should_Initialize_With_52_Cards()
        {
            // Arrange
            _deckService.ShuffleDeck(_participants);

            // Act
            var deck = _deckService.Deal(52);

            // Assert
            Assert.AreEqual(52, deck.Count);
        }

        [Test]
        public void DeckService_Should_Shuffle_Deck_With_Seed()
        {
            // Arrange
            _deckService.ShuffleDeck(_participants);

            // Act
            var deckBeforeShuffle = _deckService.Deal(52);
            _deckService.ShuffleDeck(_participants);
            var deckAfterShuffle = _deckService.Deal(52);

            // Assert
            CollectionAssert.AreNotEqual(deckBeforeShuffle, deckAfterShuffle);
        }

        [Test]
        public void DeckService_Should_Contain_All_52_Cards_After_Shuffle()
        {
            _deckService.ShuffleDeck(_participants);
            var finalizedDeck = _deckService.GetFinalPostShuffleDeckState();

            // Ensure the deck string contains 52 cards
            var cardCount = finalizedDeck.DeckString.Split(' ').Length;
            Assert.AreEqual(52, cardCount, "The final post-shuffle deck should contain 52 cards.");
        }

        [Test]
        public void DeckService_Should_Return_Completed_Deck()
        {
            // Arrange
            _deckService.ShuffleDeck(_participants);

            // Act
            var completedDeck = _deckService.GetFinalPostShuffleDeckState();

            // Assert
            Assert.IsNotNull(completedDeck);
            Assert.AreEqual(_participants.Count, completedDeck.Participants.Count);
            Assert.IsFalse(string.IsNullOrEmpty(completedDeck.BitcoinAddress));
            Assert.IsFalse(string.IsNullOrEmpty(completedDeck.DeckString));
        }

        [Test]
        public void DeckService_Should_Throw_Exception_When_Too_Many_Cards_Dealt()
        {
            // Arrange
            _deckService.ShuffleDeck(_participants);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _deckService.Deal(53), "Not enough cards in the deck to deal.");
        }

        [Test]
        public void DeckService_Should_Produce_Valid_Bitcoin_Address()
        {
            // Arrange
            _deckService.ShuffleDeck(_participants);

            // Act
            var completedDeck = _deckService.GetFinalPostShuffleDeckState();

            // Assert
            Assert.IsTrue(completedDeck.BitcoinAddress.StartsWith("1") || completedDeck.BitcoinAddress.StartsWith("3"));
        }
    }
}
