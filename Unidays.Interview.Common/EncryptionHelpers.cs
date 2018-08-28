using System;
using System.Security.Cryptography;
using System.Text;
using CryptSharp.Utility;

namespace Unidays.Interview.Common
{
    /// <summary>
    /// On a new project I would advocate against the use of third party libraries for things such as encryption of strings. However this open source package
    /// has already been checked over and the SCrypt algorithms are preferred to SHA256
    /// </summary>
    public static class EncryptionHelpers
    {
        public static string GetHashedString(string secret, string salt)
        {
            var keyBytes = Encoding.UTF8.GetBytes(secret);
            var saltBytes = Encoding.UTF8.GetBytes(salt);
            var cost = 262144;
            var blockSize = 8;
            var parallel = 1;
            var maxThreads = (int?)null;
            var output = new byte[32];

            SCrypt.ComputeKey(keyBytes, saltBytes, cost, blockSize, parallel, maxThreads, output);
            return Convert.ToBase64String(output);
        }

        public static string CreateSalt(int size)
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] buff = new byte[size];
            rng.GetBytes(buff);

            return Convert.ToBase64String(buff);
        }
    }
}
