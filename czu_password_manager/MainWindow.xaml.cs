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

namespace czu_password_manager
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MasterPassword _masterObject = new MasterPassword();
        public MainWindow()
        {
            InitializeComponent();
            
            //masterPassword.CreateHashFile();
        }

        private void Login_Button(object sender, RoutedEventArgs e)
        {
            
            CheckPassword(master.Password);
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
                    masterMatch = _masterObject.VerifyMasterPassword(password, master);

                    if (masterMatch)
                    {
                        AfterLogin afterLoginWindow = new AfterLogin();
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
                CheckPassword(master.Password);
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
            if (master.Visibility == Visibility.Visible)
            {
                masterPassword.Text = master.Password;
                master.Visibility = Visibility.Collapsed;
                masterPassword.Visibility = Visibility.Visible;
                masterBtn.Content = "X";
            }
            else
            {
                master.Password = masterPassword.Text;
                masterPassword.Visibility = Visibility.Collapsed;
                master.Visibility = Visibility.Visible;
                masterBtn.Content = "👁";
            }
        }
    }
}
