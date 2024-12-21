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
        private Credential selectedCredential = null;
        
        private Algorithms _algorithms = new Algorithms();

        public AfterLogin()
        {
            InitializeComponent();

            Credentials = new ObservableCollection<Credential>();
            LoadCredentials(); // Load credentials when the window is initialized
            
            
            DataContext = this;
        }

        // Check if vault exists
        private bool VaultExist(string vault)
        {

            return (!File.Exists(vault) ? false : true);
            
        }
        // Load credentials from the XML file
        private void LoadCredentials()
        {
            string vaultPath = _algorithms.Rot1_3(_algorithms.vaultFile);
            bool exists = VaultExist(vaultPath);
            //  if vaultExist returns false create the vault
            if (exists == false)
            {
                Vault objVault = new Vault();
                objVault.CreateVault(vaultPath);
                
            } else
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

        // Show the Add New Credential form
        private void AddCredential(object sender, RoutedEventArgs e)
        {
            nameInput.Clear();
            usernameInput.Clear();
            passwordInput.Clear();
            addNew.Visibility = Visibility.Visible;
            ClearFields();
            LockFields(false); // Unlock fields for adding new credentials
            editButton.Visibility = Visibility.Collapsed;
            deleteButton.Visibility = Visibility.Collapsed;
            saveButton.Visibility = Visibility.Visible;
        }

        // Save a new or edited credential
        private void SaveCredentials(object sender, RoutedEventArgs e)
        {
            try
            {
                string vaultName = _algorithms.Rot1_3(_algorithms.vaultFile);
                int selectedIndex = credentialsListBox.SelectedIndex;

                if (selectedCredential != null)
                {
                    // Update the existing credential
                    selectedCredential.name = nameInput.Text;
                    selectedCredential.username = usernameInput.Text;
                    selectedCredential.password = passwordInput.Password;
                }
                else
                {
                    // Add a new credential
                    Credentials.Add(new Credential
                    {
                        name = nameInput.Text,
                        username = usernameInput.Text,
                        password = passwordInput.Password
                    });
                }

                // Save updated credentials to the XML file
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Credential>));
                using (StreamWriter writer = new StreamWriter(vaultName, false))
                {
                    serializer.Serialize(writer, Credentials);
                }

                ClearFields();
                //addNew.Visibility = Visibility.Collapsed;
                //credentialsListBox.SelectedIndex = selectedIndex;
                selectedCredential = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}");
            }
        }
        private void DeleteCredential(object sender, RoutedEventArgs e)
        {
            if (selectedCredential != null)
            {
                // Confirm deletion
                var result = MessageBox.Show("Are you sure you want to delete this credential?", "Confirm Deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    // Remove the selected credential from the collection
                    Credentials.Remove(selectedCredential);

                    // Save updated credentials to the XML file
                    string vaultName = _algorithms.Rot1_3(_algorithms.vaultFile);
                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Credential>));
                        using (StreamWriter writer = new StreamWriter(vaultName, false))
                        {
                            serializer.Serialize(writer, Credentials);
                        }

                        // Clear fields and reset the form
                        ClearFields();
                        addNew.Visibility = Visibility.Collapsed;
                        selectedCredential = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while deleting the credential: {ex.Message}");
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a credential to delete.", "No Selection", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }


        // Handle selection of a credential from the ListBox
        private void selectedItem(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selectedCredential = credentialsListBox.SelectedItem as Credential;
            
            if (selectedCredential != null)
            {
                saveButton.Visibility = Visibility.Collapsed;

                nameInput.Text = selectedCredential.name;
                usernameInput.Text = selectedCredential.username;
                passwordInput.Password = selectedCredential.password;

                passwordInput.Visibility = Visibility.Visible;
                password.Visibility = Visibility.Collapsed;
                masterBtn.Content = "👁";

                addNew.Visibility = Visibility.Visible;
                editButton.Visibility = Visibility.Visible;
                deleteButton.Visibility = Visibility.Visible;

                LockFields(true); // Lock fields until "Edit" is clicked
            }
            //saveButton.Visibility = Visibility.Visible;
        }

        // Enable editing of the selected credential
        private void EditCredentials(object sender, RoutedEventArgs e)
        {
            LockFields(false); // Unlock fields for editing
            saveButton.Visibility = Visibility.Visible;
            deleteButton.Visibility = Visibility.Collapsed;
            editButton.Visibility = Visibility.Collapsed;


        }

        // Clear input fields and reset the form
        private void ClearFields()
        {
            selectedCredential = null;
            saveButton.Visibility= Visibility.Collapsed;
            deleteButton.Visibility= Visibility.Visible;
            editButton.Visibility= Visibility.Visible;
            LockFields(true);
        }

        // Lock or unlock the input fields
        private void LockFields(bool isLocked)
        {
            nameInput.IsReadOnly = isLocked;
            usernameInput.IsReadOnly = isLocked;
            passwordInput.IsEnabled = !isLocked;
        }
        private void TogglePasswordVisibility(object sender, RoutedEventArgs e)
        {
            
            if (passwordInput.Visibility == Visibility.Visible)
            {
                password.Text = passwordInput.Password;
                passwordInput.Visibility = Visibility.Collapsed;
                password.Visibility = Visibility.Visible;
                masterBtn.Content = "X";
            }
            else
            {
                passwordInput.Password = password.Text;
                password.Visibility = Visibility.Collapsed;
                passwordInput.Visibility = Visibility.Visible;
                masterBtn.Content = "👁";
            }
        }
    }
}
