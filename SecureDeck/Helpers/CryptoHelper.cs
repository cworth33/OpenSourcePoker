using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using SecureDeck.Models;
using System.Security.Cryptography;
using System.Text;

namespace SecureDeck.Helpers
{
    public class CryptoHelper
    {
        public string GetHashString(string inputString, byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: inputString,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
            return hashed;
        }

        public byte[] CombineSeeds(List<ShuffleParticipant> participants)
        {
            byte[] combinedHash = []; // Start with an empty byte array

            foreach (var participant in participants)
            {
                byte[] currentSeedHash = SHA256.HashData(Encoding.UTF8.GetBytes(participant.Seed));
                if (combinedHash.Length == 0)
                {
                    combinedHash = currentSeedHash; // Initialize with the first participant's hash
                }
                else
                {
                    combinedHash = SHA256.HashData(combinedHash.Concat(currentSeedHash).ToArray());
                }
            }

            return combinedHash;
        }

        public static string GenerateSeed()
        {
            byte[] serverSeedBytes = RandomNumberGenerator.GetBytes(32); // 256-bit seed
            return Convert.ToBase64String(serverSeedBytes);
        }
    }
}
