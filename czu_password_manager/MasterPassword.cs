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
    /*
     
     try
            {
                // Reading file -> Outputing file contents
                FileStream fileStream = File.Open(fileName, FileMode.Open, FileAccess.Read);
                byte[] fileData = new byte[fileStream.Length];
                UTF8Encoding dataStream = new UTF8Encoding();
                while (fileStream.Read(fileData, 0, fileData.Length) > 0)
                {
                    Console.WriteLine(dataStream.GetString(fileData));
                }
            }
            catch (IOException fileNotFound)
            {
                Console.WriteLine($"File {fileName} doesn't exist\n {fileNotFound.ToString()}\n {fileName} will be created.");
            }
            finally
            {
                // If file doesn't exist create it and re-run function
                if (!File.Exists(fileName))
                {

                    string loremIpsum = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, \r\nsed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, \r\nquis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat.\r\nDuis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur.\r\nExcepteur sint occaecat cupidatat non proident, \r\nsunt in culpa qui officia deserunt mollit anim id est laborum.";
                    FileStream fs = File.Create(fileName);
                    byte[] data = new UTF8Encoding().GetBytes(loremIpsum);
                    fs.Write(data, 0, data.Length);
                    fs.Close();
                    TryCatch();
                }
            }
     
     */

    internal class MasterPassword
    {
        // Hashing master password into bcrypt
        private string HashMasterPassword(string masterPassword)
        {
            string hashedMasterPassword = BCrypt.Net.BCrypt.HashPassword(masterPassword);
            return hashedMasterPassword;
        }
        // Verifying master password
        private bool VerifyMasterPassword(string masterPassword, string hasehedMasterPassword) 
        {
            return BCrypt.Net.BCrypt.Verify(masterPassword, hasehedMasterPassword);
        }

        //  Creating Master Password
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
