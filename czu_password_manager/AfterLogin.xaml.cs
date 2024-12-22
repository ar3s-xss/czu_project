using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Xml;
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
        private readonly string _masterPassword;

        public AfterLogin(string password)
        {
            InitializeComponent();
            _masterPassword = password;
            Credentials = new ObservableCollection<Credential>();
            LoadCredentials(_masterPassword); // Load credentials when the window is initialized
            this.Closing += AfterLogin_Closing;

            DataContext = this;
        }

        /// <summary>
        /// Event handler for window closing. Serializes Credentials and encrypts vault.xml.
        /// </summary>
        private void AfterLogin_Closing(object sender, CancelEventArgs e)
        {
            try
            {
                // **Step 1: Serialize the Credentials collection to vault.xml**
                SaveAllCredentials();

                string decryptedVaultPath = _algorithms.Rot1_3(_algorithms.vaultFile);
                string encryptedVaultPath = _algorithms.Rot1_3(_algorithms.encryptedVault);

                if (File.Exists(decryptedVaultPath))
                {
                    // **Step 2: Encrypt the vault.xml into vault.enc**
                    XmlEncDec.EncryptXmlVault(decryptedVaultPath, _masterPassword);

                    // **Step 3: Delete the plain-text vault.xml for security**
                    File.Delete(decryptedVaultPath);
                }
            }
            catch (Exception ex)
            {
                // Optionally show an error and keep the window open if cleanup fails
                MessageBox.Show($"Error during cleanup: {ex.Message}",
                                "Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);

                // Uncomment the following line if you want to prevent the window from closing on error
                // e.Cancel = true;
            }
        }

        /// <summary>
        /// Serializes the Credentials collection to vault.xml.
        /// Ensures that even if the collection is empty, the XML has a root element.
        /// </summary>
        private void SaveAllCredentials()
        {
            try
            {
                string vaultPath = _algorithms.Rot1_3(_algorithms.vaultFile);
                XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<Credential>));
                using (StreamWriter writer = new StreamWriter(vaultPath, false))
                {
                    serializer.Serialize(writer, Credentials);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while saving credentials: {ex.Message}",
                                "Save Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Loads credentials from the decrypted vault.xml into the Credentials collection.
        /// If the decrypted vault.xml does not exist, creates a new one with a valid root element.
        /// </summary>
        /// <param name="masterPassword">The master password for decryption.</param>
        private void LoadCredentials(string masterPassword)
        {
            try
            {
                string encryptedVaultPath = _algorithms.Rot1_3(_algorithms.encryptedVault);
                string decryptedVaultPath = _algorithms.Rot1_3(_algorithms.vaultFile);

                // **Step 1: Decrypt the vault if it exists**
                if (File.Exists(encryptedVaultPath))
                {
                    // This will produce a plain XML file at decryptedVaultPath
                    XmlEncDec.DecryptXmlVault(masterPassword, encryptedVaultPath);
                }

                // **Step 2: Check if decrypted vault.xml exists; if not, create it**
                if (!VaultExist(decryptedVaultPath))
                {
                    // **Create a new vault.xml with an empty Credentials collection**
                    Vault objVault = new Vault();
                    objVault.CreateVault(decryptedVaultPath);

                    // **Encrypt the newly created empty vault.xml**
                    XmlEncDec.EncryptXmlVault(decryptedVaultPath, masterPassword);

                    // **Delete the plain-text vault.xml after encryption**
                    File.Delete(decryptedVaultPath);
                }

                // **Step 3: Load credentials from decrypted vault.xml**
                LoadCredentialsFromFile(decryptedVaultPath);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while loading credentials: {ex.Message}",
                                "Load Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Helper method to load credentials from a specified vault.xml file.
        /// </summary>
        /// <param name="vaultPath">Path to the decrypted vault.xml file.</param>
        private void LoadCredentialsFromFile(string vaultPath)
        {
            if (File.Exists(vaultPath))
            {
                try
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
                catch (InvalidOperationException ex)
                {
                    // Handle XML deserialization errors, such as missing root elements
                    MessageBox.Show($"Failed to load credentials: {ex.Message}",
                                    "Deserialization Error",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error);

                    // Optionally, recreate the vault.xml with an empty collection
                    Vault objVault = new Vault();
                    objVault.CreateVault(vaultPath);
                }
            }
            else
            {
                // If vault.xml doesn't exist, create a new one with an empty Credentials collection
                Vault objVault = new Vault();
                objVault.CreateVault(vaultPath);
            }
        }

        /// <summary>
        /// Checks if the vault file exists.
        /// </summary>
        /// <param name="vault">Path to the vault.xml file.</param>
        /// <returns>True if exists; otherwise, false.</returns>
        private bool VaultExist(string vault)
        {
            return File.Exists(vault);
        }

        /// <summary>
        /// Shows the Add New Credential form.
        /// </summary>
        private void AddCredential(object sender, RoutedEventArgs e)
        {
            ClearFields();
            addNew.Visibility = Visibility.Visible;
            LockFields(false); // Unlock fields for adding new credentials
            editButton.Visibility = Visibility.Collapsed;
            deleteButton.Visibility = Visibility.Collapsed;
            saveButton.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// Saves a new or edited credential.
        /// </summary>
        private void SaveCredentials(object sender, RoutedEventArgs e)
        {
            try
            {
                // **Step 1: Add or update the credential in the collection**
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

                // **Step 2: Serialize the updated Credentials collection to vault.xml**
                SaveAllCredentials();

                // **Step 3: Update UI elements**
                ClearFields();
                selectedCredential = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred: {ex.Message}",
                                "Save Error",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Deletes the selected credential after confirmation.
        /// </summary>
        private void DeleteCredential(object sender, RoutedEventArgs e)
        {
            if (selectedCredential != null)
            {
                // Confirm deletion
                var result = MessageBox.Show("Are you sure you want to delete this credential?",
                                             "Confirm Deletion",
                                             MessageBoxButton.YesNo,
                                             MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    // Remove the selected credential from the collection
                    Credentials.Remove(selectedCredential);

                    // Serialize the updated Credentials collection to vault.xml
                    try
                    {
                        SaveAllCredentials();

                        // Clear fields and reset the form
                        ClearFields();
                        selectedCredential = null;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"An error occurred while deleting the credential: {ex.Message}",
                                        "Deletion Error",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please select a credential to delete.",
                                "No Selection",
                                MessageBoxButton.OK,
                                MessageBoxImage.Information);
            }
        }

        /// <summary>
        /// Handles the selection of a credential from the ListBox.
        /// Populates the input fields with the selected credential's data.
        /// </summary>
        private void selectedItem(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            selectedCredential = credentialsListBox.SelectedItem as Credential;

            if (selectedCredential != null)
            {
                // **Step 1: Populate input fields with the selected credential's data**
                nameInput.Text = selectedCredential.name;
                usernameInput.Text = selectedCredential.username;
                passwordInput.Password = selectedCredential.password;

                // **Step 2: Update UI elements for editing**
                saveButton.Visibility = Visibility.Collapsed;
                addNew.Visibility = Visibility.Visible;
                editButton.Visibility = Visibility.Visible;
                deleteButton.Visibility = Visibility.Visible;

                passwordInput.Visibility = Visibility.Visible;
                password.Visibility = Visibility.Collapsed;
                masterBtn.Content = "👁";

                LockFields(true); // Lock fields until "Edit" is clicked
            }
        }

        /// <summary>
        /// Enables editing of the selected credential.
        /// </summary>
        private void EditCredentials(object sender, RoutedEventArgs e)
        {
            LockFields(false); // Unlock fields for editing
            saveButton.Visibility = Visibility.Visible;
            deleteButton.Visibility = Visibility.Collapsed;
            editButton.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// Clears input fields and resets the form.
        /// </summary>
        private void ClearFields()
        {
            selectedCredential = null;
            nameInput.Clear();
            usernameInput.Clear();
            passwordInput.Clear();

            saveButton.Visibility = Visibility.Collapsed;
            addNew.Visibility = Visibility.Collapsed;
            deleteButton.Visibility = Visibility.Visible;
            editButton.Visibility = Visibility.Visible;

            LockFields(true);
        }

        /// <summary>
        /// Locks or unlocks the input fields.
        /// </summary>
        /// <param name="isLocked">True to lock; false to unlock.</param>
        private void LockFields(bool isLocked)
        {
            nameInput.IsReadOnly = isLocked;
            usernameInput.IsReadOnly = isLocked;
            passwordInput.IsEnabled = !isLocked;
        }

        /// <summary>
        /// Toggles the visibility of the password field.
        /// </summary>
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
