using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace AndresFlorez.RedSocial.Utilidades
{
    public class Crypto
    {
        private static readonly byte[] _clave = Encoding.ASCII.GetBytes("8!bJfP?].dfC3{yW$},uWJ9>PQ7f8exq");
        private static readonly byte[] _vector = Encoding.ASCII.GetBytes("uKnWx+3]iu$Z-b>T");
        
        /// <summary>
        /// Realiza encriptado de una cadena de texto con algoritmo AES
        /// </summary>
        /// <param name="plainText">cadena de texto a encriptar</param>
        /// <returns>Cadena encriptada</returns>
        public static string EncriptarAES(string plainText)
        {
            if (plainText == null || plainText.Length <= 0)
                throw new ArgumentNullException("texto a cifrar");
            byte[] encrypted;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = _clave;
                aesAlg.IV = _vector;

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                using MemoryStream msEncrypt = new MemoryStream();
                using CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
                using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                {
                    swEncrypt.Write(plainText);
                }
                encrypted = msEncrypt.ToArray();
            }
            return Convert.ToBase64String(encrypted);
        }

        public static string DesencriptarAES(string cipherText)
        {
            if (cipherText == null || cipherText.Length <= 0)
                throw new ArgumentNullException("Texto cifrado");
            string plaintext = null;

            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = _clave;
                aesAlg.IV = _vector;

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                using MemoryStream msDecrypt = new MemoryStream(Convert.FromBase64String(cipherText));
                using CryptoStream csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                using StreamReader srDecrypt = new StreamReader(csDecrypt);
                plaintext = srDecrypt.ReadToEnd();
            }
            return plaintext;
        }

    }
}
