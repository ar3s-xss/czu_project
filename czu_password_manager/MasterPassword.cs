using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using BCrypt.Net;
using System.IO;
using System.Windows;
namespace czu_password_manager
{
    /*
    internal class MasterPassword
    {

        // Hashing master password into bcrypt
        private string HashMasterPassword(string masterPassword)
        {
            string hashedMasterPassword = BCrypt.Net.BCrypt.HashPassword(masterPassword);
            return hashedMasterPassword;
        }
        // Verifying master password
        internal bool VerifyMasterPassword(string masterPassword, string hasehedMasterPassword) 
        {
            return BCrypt.Net.BCrypt.Verify(masterPassword, hasehedMasterPassword);
        }

        //  Creating master password
        public void CreateMasterPassword(string master)
        {
            string hashMaster = HashMasterPassword(master);
            bool verifiedMaster = VerifyMasterPassword(master, hashMaster);
            if (verifiedMaster == true)
            {
                StoreMasterPassword(hashMaster);
            }
                
        }
        private void StoreMasterPassword(string masterPassword)
        {
            Algorithms algorithms = new Algorithms();
            string fileName = algorithms.Rot1_3(algorithms.masterFile);
            try
            {

                FileStream fileStream = File.Open(fileName, FileMode.OpenOrCreate, FileAccess.Write);
                byte[] buffer = new UTF8Encoding().GetBytes(masterPassword);
                fileStream.Write(buffer, 0, buffer.Length);
                fileStream.Close();
            }
            catch (IOException e)
            {
                MessageBox.Show(e.Message);
            }
            
        }
        internal void CreateHashFile()
        {
            Algorithms algorithms = new Algorithms();
            string fileName = algorithms.Rot1_3(algorithms.masterFile);
            if (!File.Exists(fileName))
            {
                using (FileStream fileStream = File.Create(fileName))
                {
                    fileStream.Close();
                }
            }
            
        }
    }
    */
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using System.Text;

    public static class Pbkdf2PasswordManager
    {
        // Adjust these constants based on your security/performance requirements
        private const int SaltSize = 16;           // 128-bit salt
        private const int KeySize = 32;           // 256-bit subkey
        private const int Iterations = 100000;    // PBKDF2 iterations
        private static readonly HashAlgorithmName HashAlgorithm = HashAlgorithmName.SHA256;

        /// <summary>
        /// Hashes a password using PBKDF2 (Rfc2898DeriveBytes).
        /// Returns a single string containing the iteration count, salt, and subkey.
        /// </summary>
        /// <param name="password">User’s plain-text password.</param>
        /// <returns>A string in the format "Iterations:SaltBase64:SubkeyBase64".</returns>
        public static string HashPassword(string password)
        {
            // 1. Generate a random salt
            byte[] salt = new byte[SaltSize];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // 2. Derive the subkey from the password + salt
            byte[] subkey;
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, Iterations, HashAlgorithm))
            {
                subkey = pbkdf2.GetBytes(KeySize);
            }

            // 3. Convert salt and subkey to Base64
            string saltBase64 = Convert.ToBase64String(salt);
            string subkeyBase64 = Convert.ToBase64String(subkey);

            // 4. Return one string containing iteration count, salt, and subkey
            return $"{Iterations}:{saltBase64}:{subkeyBase64}";
        }

        /// <summary>
        /// Verifies a password against the stored PBKDF2 hash.
        /// </summary>
        /// <param name="password">The plain-text password provided by the user.</param>
        /// <param name="hashedPassword">The stored hashed password in the format "Iterations:Salt:Subkey".</param>
        /// <returns>True if the password is correct; otherwise, false.</returns>
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            // 1. Split the stored hash
            var parts = hashedPassword.Split(':');
            if (parts.Length != 3)
            {
                // The stored hash is in an invalid format
                return false;
            }

            // Extract iteration count, salt, and subkey
            if (!int.TryParse(parts[0], out int iterations))
            {
                return false;
            }
            byte[] salt = Convert.FromBase64String(parts[1]);
            byte[] storedSubkey = Convert.FromBase64String(parts[2]);

            // 2. Derive a new subkey from the incoming password + stored salt
            byte[] derivedSubkey;
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iterations, HashAlgorithm))
            {
                derivedSubkey = pbkdf2.GetBytes(KeySize);
            }

            // 3. Compare the stored subkey with the newly derived subkey (constant-time)
            return FixedTimeEquals(storedSubkey, derivedSubkey);
        }

        /// <summary>
        /// Saves the hashed password to a text file.
        /// </summary>
        /// <param name="hashedPassword">The hashed password string.</param>
        /// <param name="filePath">The file path to save to.</param>
        public static void SaveHashToFile(string filePath,string hashedPassword)
        {
            File.WriteAllText(filePath, hashedPassword);
        }

        /// <summary>
        /// A constant-time comparison to mitigate timing attacks.
        /// </summary>
        private static bool FixedTimeEquals(byte[] a, byte[] b)
        {
            if (a == null || b == null || a.Length != b.Length)
                return false;

            int diff = 0;
            for (int i = 0; i < a.Length; i++)
            {
                diff |= a[i] ^ b[i];
            }
            return diff == 0;
        }
    }

}
