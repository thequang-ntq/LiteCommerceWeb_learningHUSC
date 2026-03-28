using System.Security.Cryptography;
using System.Text;

namespace SV22T1020362.Shop
{
    /// <summary>
    /// Tiện ích mã hóa
    /// </summary>
    public static class CryptHelper
    {
        /// <summary>
        /// Mã hóa MD5
        /// </summary>
        public static string HashMD5(string input)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.UTF8.GetBytes(input);
                byte[] hashBytes = md5.ComputeHash(inputBytes);
                StringBuilder sb = new StringBuilder();
                foreach (byte b in hashBytes)
                    sb.Append(b.ToString("x2"));
                return sb.ToString();
            }
        }
    }
}