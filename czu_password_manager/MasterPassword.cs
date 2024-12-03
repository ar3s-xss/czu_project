using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using BCrypt.Net;
using System.IO;
namespace czu_password_manager
{

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
            const string fileName = "masterHash.txt";
            try
            {
                FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Write);
                byte[] buffer = new UTF8Encoding().GetBytes(masterPassword);
                fileStream.Write(buffer, 0, buffer.Length);
                fileStream.Close();
            }
            catch (IOException e)
            {
                Console.WriteLine(e.ToString());
            }
            finally
            {
                using (FileStream fileStream = File.Create(fileName))
                {
                    byte[] data = new UTF8Encoding().GetBytes(masterPassword);
                    fileStream.Write(data, 0, data.Length);
                    fileStream.Close();
                }
            }
        }
    }
}
