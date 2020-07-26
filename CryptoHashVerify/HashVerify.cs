using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace CryptoHashVerify
{
    public static class HashVerify
    {

        public static (string, string) GenerateHashString(string targetValue, int iterationCount = 1000, KeyDerivationPrf algorithm = KeyDerivationPrf.HMACSHA256, byte[] salt = null, bool needsOnlyHash = false)
        {
            if (salt == null || salt.Length != 16)
            {
                // generate a 128-bit salt using a secure PRNG
                salt = new byte[128 / 8];
                using var rng = RandomNumberGenerator.Create();
                rng.GetBytes(salt);
            }

            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: targetValue,
                salt: salt,
                prf: algorithm,
                iterationCount: iterationCount,
                numBytesRequested: 256 / 8));

            if (needsOnlyHash) return (hashed, string.Empty);

            return (hashed, Convert.ToBase64String(salt));
        }


        public static bool VerifyHashString(string hashedValue, string sourceSalt, string sourceToCheck, int iterationCount = 1000, KeyDerivationPrf algorithm = KeyDerivationPrf.HMACSHA256)
        {
            if (hashedValue == null || sourceSalt == null)
                return false;
            var salt = Convert.FromBase64String(sourceSalt);
            if (salt == null)
                return false;
            // hash the given source
            var (hashOfstringToCheck, returnsalt) = GenerateHashString(sourceToCheck, iterationCount, algorithm, salt, true);
            // compare both hashes
            return String.Compare(hashedValue, hashOfstringToCheck) == 0;
        }
    }
}
