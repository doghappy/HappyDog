using System;
using System.Security.Cryptography;
using System.Text;

namespace HappyDog.Infrastructure.Security
{
    public static class HashEncrypt
    {
        public static string Sha1Encrypt(string text, string salt = "", bool isEndSalt = true)
        {
            using (var sha1 = SHA1.Create())
            {
                byte[] buffer = isEndSalt
                    ? Encoding.UTF8.GetBytes(text + salt)
                    : Encoding.UTF8.GetBytes(salt + text);
                return BitConverter.ToString(sha1.ComputeHash(buffer)).Replace("-", "").ToLower();
            }
        }

        public static string Sha1Encrypt(string text)
        {
            using (var sha1 = SHA1.Create())
            {
                byte[] buffer = Encoding.UTF8.GetBytes(text);
                return BitConverter.ToString(sha1.ComputeHash(buffer)).Replace("-", "").ToLower();
            }
        }

        public static string Md5Encrypt(string text)
        {
            using (var md5 = MD5.Create())
            {
                byte[] buffer = Encoding.UTF8.GetBytes(text);
                return BitConverter.ToString(md5.ComputeHash(buffer)).Replace("-", "").ToLower();
            }
        }

        public static string Md5Encrypt(string text, string salt = "", bool isEndSalt = true)
        {
            using (var md5 = MD5.Create())
            {
                byte[] buffer = isEndSalt
                    ? Encoding.UTF8.GetBytes(text + salt)
                    : Encoding.UTF8.GetBytes(salt + text);
                return BitConverter.ToString(md5.ComputeHash(buffer)).Replace("-", "").ToLower();
            }
        }
    }
}
