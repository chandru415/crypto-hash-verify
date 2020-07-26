using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Security.Cryptography;

namespace CryptoHashVerify
{
    public static class HashVerify
    {
        /// <summary>
        /// Generate hash value using Password-Based Key Derivation Function 2
        /// </summary>
        /// <param name="targetValue">string - Value to be hash</param>
        /// <param name="iterationCount">integer - Number of iteratation, default is 1000 </param>
        /// <param name="algorithm">KeyDerivationPrf - Enum type of KeyDerivationPrf, default is HMACSHA256 </param>
        /// <param name="salt">byte[] - 128/8 byte code, default is null</param>
        /// <param name="needsOnlyHash">boolean - condition to return hash</param>
        /// <returns>type Tuple - (hashed value, salt value)</returns>
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

        /// <summary>
        /// Verify given hashed value with plan string value after hashing with same settings,
        /// that was provided on hash
        /// </summary>
        /// <param name="hashedValue">string - Encrypted value</param>
        /// <param name="sourceSalt">string - Based 64 string</param>
        /// <param name="sourceToCheck">string - plan text</param>
        /// <param name="iterationCount">integer - Number of iteratation, default is 1000 </param>
        /// <param name="algorithm">Enum type of KeyDerivationPrf, default is HMACSHA256</param>
        /// <returns>boolean - return true if comparision success otherwise returns false</returns>
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
