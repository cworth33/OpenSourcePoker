using DeckValidationServcie.Controllers;
using DeckValidationService.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using SecureDeck.Interfaces;
using SecureDeck.Models;
using System;
using System.Collections.Generic;

namespace DeckValidationServcie.Tests
{
    [TestFixture]
    public class DeckValidationControllerTest
    {
        private Mock<IDeckService> _mockDeckService;
        private Mock<IDeckValidator> _mockValidator;
        private DeckValidationController _controller;

        [SetUp]
        public void Setup()
        {
            _mockDeckService = new Mock<IDeckService>();
            _mockValidator = new Mock<IDeckValidator>();
            _controller = new DeckValidationController(_mockDeckService.Object, _mockValidator.Object);
        }

        [TearDown]
        public void TearDown()
        {
            _controller.Dispose();
        }

        [Test]
        public void GetDeck_ValidParticipants_ReturnsOkResult()
        {
            // Arrange
            var participants = new List<ShuffleParticipant>
            {
                new ShuffleParticipant { Name = "Operator", Seed = "opSeed" },
                new ShuffleParticipant { Name = "Player1", Seed = "seed1" }
            };

            var expectedDeck = new ShuffledDeck
            {
                DeckString = "mockedDeckString",
                Participants = participants,
                BitcoinAddress = "mockedBitcoinAddress"
            };

            _mockDeckService.Setup(ds => ds.GetFinalPostShuffleDeckState())
                            .Returns(expectedDeck);

            // Act
            var result = _controller.GetDeck(participants) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var deck = result.Value as ShuffledDeck;
            Assert.AreEqual("mockedDeckString", deck.DeckString);
            Assert.AreEqual("mockedBitcoinAddress", deck.BitcoinAddress);
            _mockDeckService.Verify(ds => ds.ShuffleDeck(participants), Times.Once);
        }

        [Test]
        public void GetDeck_WhenExceptionThrown_ReturnsBadRequest()
        {
            // Arrange
            var participants = new List<ShuffleParticipant>
            {
                new ShuffleParticipant { Name = "Player1", Seed = "seed1" }
            };

            _mockDeckService.Setup(ds => ds.ShuffleDeck(It.IsAny<List<ShuffleParticipant>>()))
                            .Throws(new Exception("Test Exception"));

            // Act
            var result = _controller.GetDeck(participants) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }

        [Test]
        public void ValidateDeck_ValidRequest_ReturnsOkResult()
        {
            // Arrange
            var shuffledDeck = new ShuffledDeck
            {
                DeckString = "mockedDeckString",
                Participants = new List<ShuffleParticipant>
                {
                    new ShuffleParticipant { Name = "Operator", Seed = "opSeed" },
                    new ShuffleParticipant { Name = "Player1", Seed = "seed1" }
                },
                BitcoinAddress = "mockedBitcoinAddress"
            };

            var validationResult = new ValidateDeckResult
            {
                Request = shuffledDeck,
                ResultingBitcoinAddress = "mockedBitcoinAddress",
                IsValid = true,
                ValidatedDate = DateTime.UtcNow
            };

            _mockValidator.Setup(v => v.ValidateDeck(It.IsAny<ShuffledDeck>()))
                          .Returns(validationResult);

            // Act
            var result = _controller.ValidateDeck(shuffledDeck) as OkObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<OkObjectResult>(result);
            var validation = result.Value as ValidateDeckResult;
            Assert.IsTrue(validation.IsValid);
            Assert.AreEqual("mockedBitcoinAddress", validation.ResultingBitcoinAddress);
        }

        [Test]
        public void ValidateDeck_WhenExceptionThrown_ReturnsBadRequest()
        {
            // Arrange
            var shuffledDeck = new ShuffledDeck
            {
                DeckString = "mockedDeckString",
                Participants = new List<ShuffleParticipant>
                {
                    new ShuffleParticipant { Name = "Operator", Seed = "opSeed" },
                    new ShuffleParticipant { Name = "Player1", Seed = "seed1" }
                },
                BitcoinAddress = "mockedBitcoinAddress"
            };

            _mockValidator.Setup(v => v.ValidateDeck(It.IsAny<ShuffledDeck>()))
                          .Throws(new Exception("Validation Failed"));

            // Act
            var result = _controller.ValidateDeck(shuffledDeck) as BadRequestObjectResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
        }
    }
}
