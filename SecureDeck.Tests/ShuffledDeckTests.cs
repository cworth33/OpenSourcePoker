using SecureDeck.Models;

namespace SecureDeck.Tests
{
    [TestFixture]
    public class ShuffledDeckTests
    {
        [Test]
        public void ShuffledDeck_Should_Initialize_With_Correct_Values()
        {
            // Arrange
            var deckString = "AsKsQsJsTs9s8s7s6s5s4s3s2s";
            var participants = new List<ShuffleParticipant>
            {
                new ShuffleParticipant { Name = "Player1", Seed = "seed1" },
                new ShuffleParticipant { Name = "Player2", Seed = "seed2" }
            };
            var bitcoinAddress = "1A1zP1eP5QGefi2DMPTfTL5SLmv7DivfNa";

            // Act
            var shuffledDeck = new ShuffledDeck
            {
                DeckString = deckString,
                Participants = participants,
                BitcoinAddress = bitcoinAddress
            };

            // Assert
            Assert.AreEqual(deckString, shuffledDeck.DeckString);
            Assert.AreEqual(participants, shuffledDeck.Participants);
            Assert.AreEqual(bitcoinAddress, shuffledDeck.BitcoinAddress);
        }

        [Test]
        public void ShuffledDeck_Should_Have_Required_Participants()
        {
            // Arrange
            var deckString = "AsKsQsJsTs9s8s7s6s5s4s3s2s";
            var participants = new List<ShuffleParticipant>
            {
                new ShuffleParticipant { Name = "Player1", Seed = "seed1" }
            };
            var bitcoinAddress = "1A1zP1eP5QGefi2DMPTfTL5SLmv7DivfNa";

            // Act
            var shuffledDeck = new ShuffledDeck
            {
                DeckString = deckString,
                Participants = participants,
                BitcoinAddress = bitcoinAddress
            };

            // Assert
            Assert.IsNotNull(shuffledDeck.Participants);
            Assert.AreEqual(1, shuffledDeck.Participants.Count);
            Assert.AreEqual("Player1", shuffledDeck.Participants[0].Name);
            Assert.AreEqual("seed1", shuffledDeck.Participants[0].Seed);
        }

        [Test]
        public void ShuffledDeck_Should_Have_Valid_BitcoinAddress()
        {
            // Arrange
            var bitcoinAddress = "1A1zP1eP5QGefi2DMPTfTL5SLmv7DivfNa";

            // Act
            var shuffledDeck = new ShuffledDeck
            {
                DeckString = "AsKsQsJsTs9s8s7s6s5s4s3s2s",
                Participants = new List<ShuffleParticipant>
                {
                    new ShuffleParticipant { Name = "Player1", Seed = "seed1" }
                },
                BitcoinAddress = bitcoinAddress
            };

            // Assert
            Assert.AreEqual(bitcoinAddress, shuffledDeck.BitcoinAddress);
        }
    }
}
