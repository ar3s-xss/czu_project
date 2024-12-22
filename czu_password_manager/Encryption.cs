using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using System.Dynamic;
using System.IO;
using System.Windows.Media.Animation;
using System.Xml;

namespace czu_password_manager
{
    internal class XmlEncDec
    {
        private const int _saltSize = 16;
        private const int _keySize = 32;
        private const int _iterations = 100000;

        public static void EncryptXmlVault(string xmlPath, string password)
        {
            Algorithms algorithms = new Algorithms();
            //CreateEncryptedFile(algorithms.Rot1_3(algorithms.encryptedVault));
            string xmlContent = String.Empty;
            if (File.Exists(xmlPath)) 
            {
                xmlContent = File.ReadAllText(algorithms.Rot1_3(algorithms.vaultFile));
            }
            byte[] plainBytes = Encoding.UTF8.GetBytes(xmlContent);
            byte[] salt = new byte[_saltSize];

            // 2. Generate a random salt
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            // 3. Derive an AES key + IV from the password + salt
            byte[] key, iv;
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, _iterations, HashAlgorithmName.SHA256))
            {
                key = pbkdf2.GetBytes(_keySize); // AES-256 key
                iv = pbkdf2.GetBytes(16);      // 128-bit IV
            }

            // 4. Encrypt using AES in CBC mode
            byte[] cipherBytes;
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv;
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (var encryptor = aes.CreateEncryptor())
                    using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(plainBytes, 0, plainBytes.Length);
                        csEncrypt.FlushFinalBlock();
                        cipherBytes = msEncrypt.ToArray();
                    }
                }
            }

            // 5. Combine salt + IV + ciphertext
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(salt, 0, salt.Length);
                ms.Write(iv, 0, iv.Length);
                ms.Write(cipherBytes, 0, cipherBytes.Length);

                // 6. Write the combined bytes to the encrypted vault file
                File.WriteAllBytes(algorithms.Rot1_3(algorithms.encryptedVault), ms.ToArray());
            }

        }
        public static void DecryptXmlVault(string password, string encryptedVault)
        {
            Algorithms algorithms = new Algorithms();
            byte[] fileBytes = File.ReadAllBytes(encryptedVault);
            if (fileBytes.Length < _saltSize + 16)
            {
                throw new ArgumentException("Encrypted file is too short or corrupted.");
            }

            // 1. Extract salt
            byte[] salt = new byte[_saltSize];
            Array.Copy(fileBytes, 0, salt, 0, salt.Length);

            // 2. Extract IV
            byte[] iv = new byte[16];
            Array.Copy(fileBytes, _saltSize, iv, 0, iv.Length);

            // 3. Extract ciphertext
            byte[] cipherBytes = new byte[fileBytes.Length - _saltSize - iv.Length];
            Array.Copy(fileBytes, _saltSize + iv.Length, cipherBytes, 0, cipherBytes.Length);

            // 4. Re-derive the key from the password + salt
            byte[] key, derivedIV;
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, _iterations, HashAlgorithmName.SHA256))
            {
                key = pbkdf2.GetBytes(_keySize);
                derivedIV = pbkdf2.GetBytes(16);
            }

            // 5. Decrypt
            byte[] decryptedBytes;
            using (Aes aes = Aes.Create())
            {
                aes.Key = key;
                aes.IV = iv; // we stored it explicitly
                aes.Mode = CipherMode.CBC;
                aes.Padding = PaddingMode.PKCS7;

                using (MemoryStream msDecrypt = new MemoryStream())
                {
                    using (var decryptor = aes.CreateDecryptor())
                    using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Write))
                    {
                        csDecrypt.Write(cipherBytes, 0, cipherBytes.Length);
                        csDecrypt.FlushFinalBlock();
                        decryptedBytes = msDecrypt.ToArray();
                    }
                }
            }

            // 6. Convert bytes to string and verify it's valid XML
            string decryptedXml = Encoding.UTF8.GetString(decryptedBytes);
            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(decryptedXml);

            // 7. Save to plain XML file
            xmlDoc.Save(algorithms.Rot1_3(algorithms.vaultFile));
        }

        
    }
    
}
