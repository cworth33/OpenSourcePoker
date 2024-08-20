using SecureDeck.Helpers;
using SecureDeck.Models;
using System.Text;

namespace SecureDeck.Tests.Helpers
{
    [TestFixture]
    public class CryptoHelperTests
    {
        private CryptoHelper _cryptoHelper;

        [SetUp]
        public void SetUp()
        {
            _cryptoHelper = new CryptoHelper();
        }

        [Test]
        public void GetHashString_Should_Return_Correct_Hash()
        {
            // Arrange
            var inputString = "testInput";
            var salt = Encoding.UTF8.GetBytes("testSalt");

            // Act
            var hash = _cryptoHelper.GetHashString(inputString, salt);

            // Assert
            Assert.IsNotNull(hash);
            Assert.IsNotEmpty(hash);
        }

        [Test]
        public void CombineSeeds_Should_Return_Combined_Seed_Hash()
        {
            // Arrange
            var participants = new List<ShuffleParticipant>
            {
                new ShuffleParticipant { Name = "Player1", Seed = "Seed1" },
                new ShuffleParticipant { Name = "Player2", Seed = "Seed2" },
                new ShuffleParticipant { Name = "Player3", Seed = "Seed3" }
            };

            // Act
            var combinedSeed = _cryptoHelper.CombineSeeds(participants);

            // Assert
            Assert.IsNotNull(combinedSeed);
            Assert.IsNotEmpty(combinedSeed);
            Assert.AreEqual(32, combinedSeed.Length); // SHA-256 produces a 32-byte hash
        }

        [Test]
        public void GenerateSeed_Should_Return_Valid_Seed_String()
        {
            // Act
            var seed = CryptoHelper.GenerateSeed();

            // Assert
            Assert.IsNotNull(seed);
            Assert.IsNotEmpty(seed);
            Assert.AreEqual(44, seed.Length); // Base64 string length for 32-byte input is 44 characters
        }

        [Test]
        public void CombineSeeds_Should_Return_Different_Results_For_Different_Participants()
        {
            // Arrange
            var participants1 = new List<ShuffleParticipant>
            {
                new ShuffleParticipant { Name = "Player1", Seed = "Seed1" },
                new ShuffleParticipant { Name = "Player2", Seed = "Seed2" }
            };

            var participants2 = new List<ShuffleParticipant>
            {
                new ShuffleParticipant { Name = "Player1", Seed = "Seed1" },
                new ShuffleParticipant { Name = "Player2", Seed = "Seed2" },
                new ShuffleParticipant { Name = "Player3", Seed = "Seed3" }
            };

            // Act
            var combinedSeed1 = _cryptoHelper.CombineSeeds(participants1);
            var combinedSeed2 = _cryptoHelper.CombineSeeds(participants2);

            // Assert
            Assert.IsNotNull(combinedSeed1);
            Assert.IsNotNull(combinedSeed2);
            Assert.AreNotEqual(combinedSeed1, combinedSeed2);
        }

        [Test]
        public void GetHashString_Should_Return_Different_Hash_For_Different_Salts()
        {
            // Arrange
            var inputString = "testInput";
            var salt1 = Encoding.UTF8.GetBytes("testSalt1");
            var salt2 = Encoding.UTF8.GetBytes("testSalt2");

            // Act
            var hash1 = _cryptoHelper.GetHashString(inputString, salt1);
            var hash2 = _cryptoHelper.GetHashString(inputString, salt2);

            // Assert
            Assert.AreNotEqual(hash1, hash2);
        }
    }
}
