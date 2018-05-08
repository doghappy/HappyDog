using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace HappyDog.Infrastructure.Security
{
    /// <summary>
    /// AES加密解密 
    /// AES加密模式：CBC;
    /// 填充：pkcs5padding;
    /// 数据块：128;
    /// </summary>
    public static class AESEncrypt
    {
        static readonly byte[] IV = Encoding.UTF8.GetBytes("HappyDogHeroWong");

        public static string Encrypt(string plainText, string key)
        {
            byte[] bufferKey = Encoding.UTF8.GetBytes(key);

            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("plainText");
            if (bufferKey == null || bufferKey.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");
            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = bufferKey;
                aesAlg.IV = IV;
                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msEncrypt = new MemoryStream())
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(plainText);
                    }
                    encrypted = msEncrypt.ToArray();
                }
            }
            return Convert.ToBase64String(encrypted);
        }

        public static string Decrypt(string cipherText, string key)
        {
            byte[] bufferKey = Encoding.UTF8.GetBytes(key);
            byte[] bufferText = Convert.FromBase64String(cipherText);

            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("cipherText");
            if (bufferKey == null || bufferKey.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("IV");

            string plaintext = null;
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = bufferKey;
                aesAlg.IV = IV;
                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using (MemoryStream msDecrypt = new MemoryStream(bufferText))
                using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                using (StreamReader srDecrypt = new StreamReader(csDecrypt))
                {
                    plaintext = srDecrypt.ReadToEnd();
                }
            }
            return plaintext;
        }
    }
}
