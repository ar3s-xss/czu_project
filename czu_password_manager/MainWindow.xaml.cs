using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace czu_password_manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private MasterPassword _masterObject = new MasterPassword();
        public MainWindow()
        {
            InitializeComponent();
            //masterPassword.CreateHashFile();
            
        }
        
        private void Login_Button(object sender, RoutedEventArgs e)
        {
            
            CheckPassword(masterButton.Password);
            
            
        }
        // Provizorni kontrola hesla
        private void CheckPassword(string password)
        {
            
            Algorithms algObject = new Algorithms();
            bool masterMatch;

            // Check if the password is null or empty
            if (string.IsNullOrEmpty(password))
            {
                errorLbl.Visibility = Visibility.Visible;
                errorLbl.Content = "Master password isn't yet set \U0001F603";
            }
            else
            {
                try
                {
                    string master = File.ReadAllText(algObject.Rot1_3(algObject.masterFile));
                    masterMatch = Pbkdf2PasswordManager.VerifyPassword(password, master);//    _masterObject.VerifyMasterPassword(password, master);

                    if (masterMatch)
                    {
                
                        //  XmlEncDec.EncryptXmlVault(algObject.Rot1_3(algObject.vaultFile), password);
                        if (File.Exists(algObject.Rot1_3(algObject.encryptedVault))){
                            XmlEncDec.DecryptXmlVault(password, algObject.Rot1_3(algObject.encryptedVault));
                        }
                            
                        AfterLogin afterLoginWindow = new AfterLogin(password);
                        afterLoginWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        errorLbl.Visibility = Visibility.Visible;
                        errorLbl.Content = "Wrong master password!";
                    }
                }
                catch (FileNotFoundException)
                {
                    errorLbl.Visibility = Visibility.Visible;
                    errorLbl.Content = "Master password file not found!";
                }
                catch (Exception ex)
                {
                    errorLbl.Visibility = Visibility.Visible;
                    errorLbl.Content = $"An error occurred: {ex.Message}";
                }
            }
        }

        private void password_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) 
            {
                CheckPassword(masterButton.Password);
            }
        }

        private void CreateAccount_Click(object sender, RoutedEventArgs e)
        {
            UnleashMaster unleashMasterWindow = new UnleashMaster();
            unleashMasterWindow.doesMasterExist();
            unleashMasterWindow.Show();
            
            this.Close();
        }
        
        private void ToggleMasterVisibility(object sender, RoutedEventArgs e)
        {
            if (masterButton.Visibility == Visibility.Visible)
            {
                masterPassword.Text = masterButton.Password;
                masterButton.Visibility = Visibility.Collapsed;
                masterPassword.Visibility = Visibility.Visible;
                masterBtn.Content = "X";
            }
            else
            {
                masterButton.Password = masterPassword.Text;
                masterPassword.Visibility = Visibility.Collapsed;
                masterButton.Visibility = Visibility.Visible;
                masterBtn.Content = "👁";
            }
        }
    }
}
