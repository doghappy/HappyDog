using System;
using System.Security.Cryptography;
using System.Text;

namespace HappyDog.Infrastructure.Security
{
    public static class HashEncryptor
    {
        public static string HmacSha1(string key, string text)
        {
            byte[] keyByte = Encoding.UTF8.GetBytes(key);
            byte[] messageBytes = Encoding.UTF8.GetBytes(text);
            using (var hmacSha1 = new HMACSHA1(keyByte))
            {
                byte[] hashmessage = hmacSha1.ComputeHash(messageBytes);
                return BitConverter.ToString(hashmessage).Replace("-", "").ToLower();
            }
        }
    }
}
