using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
            LoadCredentials();
            DataContext = this;
            
        }

        public void ChangeLabel(string message)
        {
            //label2.Content = message;
        }

        private void AddNew(object sender, RoutedEventArgs e)
        {
            addNew.Visibility = Visibility.Visible;
        }

        private void LoadCredentials()
        {
            string vaultPath = "vault.xml";
            Credentials = new ObservableCollection<Credential>();
            if (File.Exists(vaultPath))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Credential>));
                using (StreamReader reader = new StreamReader(vaultPath)) 
                {
                    var loadedCredentials = (ObservableCollection<Credential>)serializer.Deserialize(reader);
                    Credentials.Clear();
                    foreach (var credential in loadedCredentials)
                    {
                        Credentials.Add(credential);
                    }
                }

            }

        }
        private void SaveCredentials(object sender, RoutedEventArgs e)
        {
            
            try
            {
                string vaultName = "vault.xml";

                Credentials.Add(new Credential
                {
                    name = nameInput.Text,
                    username = usernameInput.Text,
                    password = passwordInput.Password
                });
                
                XmlSerializer serializer2 = new XmlSerializer(typeof(ObservableCollection<Credential>));
                using(StreamWriter writer = new StreamWriter(vaultName, false))
                {
                    serializer2.Serialize(writer, Credentials);
                }

                nameInput.Clear();
                usernameInput.Clear();
                passwordInput.Clear();
                addNew.Visibility = Visibility.Collapsed;
                credentialsListBox.ItemsSource = null;
                credentialsListBox.ItemsSource = Credentials;


            } catch(Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
            

        }
    }
}
