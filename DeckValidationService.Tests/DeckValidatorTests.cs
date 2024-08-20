using NUnit.Framework;
using DeckValidationServcie.Services;
using SecureDeck.Models;
using SecureDeck.Helpers;
using System.Collections.Generic;
using SecureDeck.Services;

namespace DeckValidationService.Tests
{
    [TestFixture]
    public class DeckValidatorTests
    {
        private DeckService _deckService;
        private DeckValidator _deckValidator;

        [SetUp]
        public void Setup()
        {
            _deckService = new DeckService();
            _deckValidator = new DeckValidator();
        }

        [Test]
        public void ValidateDeck_ShouldReturnTrueForValidDeck()
        {
            // Arrange
            var participants = new List<ShuffleParticipant>
            {
                new ShuffleParticipant { Name = "Operator", Seed = "random-operator-seed" },
                new ShuffleParticipant { Name = "Player1", Seed = "seed1" },
                new ShuffleParticipant { Name = "Player2", Seed = "seed2" }
            };

            // Act
            _deckService.ShuffleDeck(participants);
            var shuffledDeck = _deckService.GetFinalPostShuffleDeckState();
            var validationResult = _deckValidator.ValidateDeck(shuffledDeck);

            // Assert
            Assert.IsTrue(validationResult.IsValid, "The deck should be valid.");
            Assert.AreEqual(shuffledDeck.BitcoinAddress, validationResult.ResultingBitcoinAddress, "The Bitcoin addresses should match.");
        }

        [Test]
        public void ValidateDeck_InvalidDeck_ReturnsFalse()
        {
            // Arrange
            var shuffledDeck = new ShuffledDeck
            {
                DeckString = "2h 3d 4s 5c 6h 7d 8s 9c Th Jd Qs Kc",
                Participants = new List<ShuffleParticipant>
                {
                    new ShuffleParticipant { Name = "Player1", Seed = "seed1" },
                    new ShuffleParticipant { Name = "Player2", Seed = "seed2" }
                },
                BitcoinAddress = "invalidBitcoinAddress"
            };

            // Act
            var result = _deckValidator.ValidateDeck(shuffledDeck);

            // Assert
            Assert.IsFalse(result.IsValid, "The deck validation should return false for an invalid deck.");
        }

        [Test]
        public void ValidateDeck_EmptyParticipants_ThrowsException()
        {
            // Arrange
            var shuffledDeck = new ShuffledDeck
            {
                DeckString = "2h 3d 4s 5c 6h 7d 8s 9c Th Jd Qs Kc Ah",
                Participants = new List<ShuffleParticipant>(),
                BitcoinAddress = "validBitcoinAddress"
            };

            // Act & Assert
            Assert.Throws<ArgumentException>(() => _deckValidator.ValidateDeck(shuffledDeck),
                "An exception should be thrown if no participants are provided.");
        }

        [Test]
        public void ValidateDeck_MissingDeckString_ReturnsFalse()
        {
            // Arrange
            var shuffledDeck = new ShuffledDeck
            {
                DeckString = "",
                Participants = new List<ShuffleParticipant>
                {
                    new ShuffleParticipant { Name = "Player1", Seed = "seed1" },
                    new ShuffleParticipant { Name = "Player2", Seed = "seed2" }
                },
                BitcoinAddress = "validBitcoinAddress"
            };

            // Act
            var result = _deckValidator.ValidateDeck(shuffledDeck);

            // Assert
            Assert.IsFalse(result.IsValid, "The deck validation should return false if the deck string is empty.");
        }
    }
}
