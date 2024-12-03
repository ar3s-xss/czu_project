using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

namespace czu_password_manager
{
    /// <summary>
    /// Interaction logic for UnleashMaster.xaml
    /// </summary>
    public partial class UnleashMaster : Window
    {
        public UnleashMaster()
        {
            InitializeComponent();
        }
        const string fileName = "masterHash.txt";
        private bool setCreate = false;
        public void PasswordSet()
        {
            
        }
        public void doesMasterExist()
        {
            

            byte[] bytes = File.ReadAllBytes(fileName);
            if (!File.Exists(fileName) || bytes.Length > 0)
            {
                oldPassLbl.Visibility = Visibility.Visible;
                oldPassGrid.Visibility = Visibility.Visible;
                setCreate = true;
            }
            else
            {
                oldPassLbl.Visibility = Visibility.Collapsed;
                oldPassGrid.Visibility = Visibility.Collapsed;
                setCreate = false;
            }
        }
        public void PasswordChange() 
        {

            string master = File.ReadAllText(fileName);

                
        }
        private void SetChangeMaster(object sender, RoutedEventArgs e)
        {
            MasterPassword masterObject = new MasterPassword();
            if (setCreate == true)
            {
                string master = File.ReadAllText(fileName);
                bool isMaster = masterObject.VerifyMasterPassword(oldPassword.Password, master);
                if (isMaster == true && !(newPassword.Password == string.Empty || againNewPassword.Password == string.Empty))
                {
                    if (newPassword.Password == againNewPassword.Password)
                    {
                        masterObject.CreateMasterPassword(newPassword.Password);
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                } else if (isMaster && !(newPassword.Password == againNewPassword.Password))
                {
                    errorLbl.Content = "Passwords do not match!";
                } else
                {
                    errorLbl.Content = "Wrong master password!";
                }
                
            }  
            // if master password doesnt exist -> master will be created
            else 
            {
                if (!(newPassword.Password == string.Empty || againNewPassword.Password == string.Empty)) 
                {
                    if (newPassword.Password == againNewPassword.Password)
                    {
                        masterObject.CreateMasterPassword(newPassword.Password);
                        MainWindow mainWindow = new MainWindow();
                        mainWindow.Show();
                        this.Close();
                    }
                    else
                    {
                        errorLbl.Content = "Master passwords do not match!";
                    }
                } else
                {
                    errorLbl.Content = "Password/s cannot be empty!";
                }
            }
        }
        private void ToggleOldPasswordVisibility(object sender, RoutedEventArgs e)
        {
            if (oldPassword.Visibility == Visibility.Visible)
            {
                oldPasswordTextBox.Text = oldPassword.Password;
                oldPassword.Visibility = Visibility.Collapsed;
                oldPasswordTextBox.Visibility = Visibility.Visible;
                oldBtn.Content = "X";
            }
            else
            {
                oldPassword.Password = oldPasswordTextBox.Text;
                oldPasswordTextBox.Visibility = Visibility.Collapsed;
                oldPassword.Visibility = Visibility.Visible;
                oldBtn.Content = "👁";
            }
        }

        private void ToggleNewPasswordVisibility(object sender, RoutedEventArgs e)
        {
            if (newPassword.Visibility == Visibility.Visible)
            {
                newPasswordTextBox.Text = newPassword.Password;
                newPassword.Visibility = Visibility.Collapsed;
                newPasswordTextBox.Visibility = Visibility.Visible;
                newBtn.Content = "X";
            }
            else
            {
                newPassword.Password = newPasswordTextBox.Text;
                newPasswordTextBox.Visibility = Visibility.Collapsed;
                newPassword.Visibility = Visibility.Visible;
                newBtn.Content = "👁";
            }
        }

        private void ToggleAgainPasswordVisibility(object sender, RoutedEventArgs e)
        {
            if (againNewPassword.Visibility == Visibility.Visible)
            {
                againNewPasswordTextBox.Text = againNewPassword.Password;
                againNewPassword.Visibility = Visibility.Collapsed;
                againNewPasswordTextBox.Visibility = Visibility.Visible;
                aNewBtn.Content = "X";
            }
            else
            {
                againNewPassword.Password = againNewPasswordTextBox.Text;
                againNewPasswordTextBox.Visibility = Visibility.Collapsed;
                againNewPassword.Visibility = Visibility.Visible;
                aNewBtn.Content = "👁";
            }
        }
    }
}
