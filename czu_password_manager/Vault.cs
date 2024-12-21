using System;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace czu_password_manager
{
    internal class Vault
    {
        internal void CreateVault(string vaultPath)
        {
            using (FileStream fileStream = File.Create(vaultPath))
            {
                fileStream.Close();
            }
        }
    }

    public class Credential : INotifyPropertyChanged
    {
        private string _name;
        private string _username;
        private string _password;

        public string name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(name));
                }
            }
        }

        public string username
        {
            get => _username;
            set
            {
                if (_username != value)
                {
                    _username = value;
                    OnPropertyChanged(nameof(username));
                }
            }
        }

        public string password
        {
            get => _password;
            set
            {
                if (_password != value)
                {
                    _password = value;
                    OnPropertyChanged(nameof(password));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
