using SecureDeck.Models;

namespace SecureDeck.Tests
{
    [TestFixture]
    public class CardTests
    {
        [Test]
        public void Card_Should_Initialize_Correctly()
        {
            // Arrange
            var rank = RankType.Ace;
            var suit = SuitType.Spades;

            // Act
            var card = new Card(rank, suit);

            // Assert
            Assert.AreEqual(rank, card.Rank);
            Assert.AreEqual(suit, card.Suit);
        }

        [Test]
        public void Card_ToString_Should_Return_Correct_FullName()
        {
            // Arrange
            var card = new Card(RankType.King, SuitType.Clubs);

            // Act
            var result = card.ToString();

            // Assert
            Assert.AreEqual("King of Clubs", result);
        }

        [Test]
        public void Card_ToShortString_Should_Return_Correct_ShortName()
        {
            // Arrange
            var card = new Card(RankType.Ten, SuitType.Hearts);

            // Act
            var result = card.ToShortString();

            // Assert
            Assert.AreEqual("Th", result);
        }

        [Test]
        public void Card_ToShortString_Should_Return_Correct_ShortName_For_Ace_Of_Spades()
        {
            // Arrange
            var card = new Card(RankType.Ace, SuitType.Spades);

            // Act
            var result = card.ToShortString();

            // Assert
            Assert.AreEqual("As", result);
        }
    }
}
