using SecureDeck.Models;

namespace SecureDeck.Tests
{
    [TestFixture]
    public class DeckTests
    {
        [Test]
        public void Deck_Should_Initialize_Empty()
        {
            // Arrange & Act
            var deck = new Deck();

            // Assert
            Assert.IsNotNull(deck.Cards);
            Assert.AreEqual(0, deck.Count);
        }

        [Test]
        public void Deck_Should_Initialize_With_52_Cards()
        {
            // Arrange
            var cards = CreateStandardDeck();
            var deck = new Deck();

            // Act
            deck.InitializeDeck(cards);

            // Assert
            Assert.AreEqual(52, deck.Count);
            Assert.AreEqual(cards, deck.Cards);
        }

        [Test]
        public void Deck_Count_Should_Return_Correct_Number_Of_Cards()
        {
            // Arrange
            var cards = CreateStandardDeck();
            var deck = new Deck();
            deck.InitializeDeck(cards);

            // Act
            var count = deck.Count;

            // Assert
            Assert.AreEqual(52, count);
        }

        // Helper method to create a standard 52-card deck
        private List<Card> CreateStandardDeck()
        {
            var ranks = new[] { RankType.Two, RankType.Three, RankType.Four, RankType.Five, RankType.Six, RankType.Seven, RankType.Eight, RankType.Nine, RankType.Ten, RankType.Jack, RankType.Queen, RankType.King, RankType.Ace };
            var suits = new[] { SuitType.Hearts, SuitType.Diamonds, SuitType.Clubs, SuitType.Spades };
            var cards = new List<Card>();

            foreach (var suit in suits)
            {
                foreach (var rank in ranks)
                {
                    cards.Add(new Card(rank, suit));
                }
            }

            return cards;
        }
    }
}
