using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Xml.Serialization;

namespace czu_password_manager
{
    /// <summary>
    /// Interaction logic for AfterLogin.xaml
    /// </summary>
    public partial class AfterLogin : Window
    {
        public ObservableCollection<Credential> Credentials { get; set; }

        public AfterLogin()
        {
            InitializeComponent();

            Credentials = new ObservableCollection<Credential>();
            LoadCredentials(); // Load credentials when the window is initialized
            DataContext = this;
        }

        private void AddNew(object sender, RoutedEventArgs e)
        {
            addNew.Visibility = Visibility.Visible; // Show the Add New Credential form
        }

        private void LoadCredentials()
        {
            string vaultPath = "vault.xml";

            // Only load from file if it exists
            if (File.Exists(vaultPath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Credential>));
                using (StreamReader reader = new StreamReader(vaultPath))
                {
                    var loadedCredentials = (ObservableCollection<Credential>)serializer.Deserialize(reader);
                    Credentials.Clear(); // Clear current credentials
                    foreach (var credential in loadedCredentials)
                    {
                        Credentials.Add(credential); // Add loaded credentials to ObservableCollection
                    }
                }
            }
        }

        private void SaveCredentials(object sender, RoutedEventArgs e)
        {
            try
            {
                string vaultName = "vault.xml";

                // Add the new credential to the ObservableCollection
                Credentials.Add(new Credential
                {
                    name = nameInput.Text,
                    username = usernameInput.Text,
                    password = passwordInput.Password
                });

                // Save updated credentials back to the XML file
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Credential>));
                using (StreamWriter writer = new StreamWriter(vaultName, false)) // Overwrite the file
                {
                    serializer.Serialize(writer, Credentials);
                }

                // Clear the input fields and hide the form
                nameInput.Clear();
                usernameInput.Clear();
                passwordInput.Clear();
                addNew.Visibility = Visibility.Collapsed;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
    }
}
