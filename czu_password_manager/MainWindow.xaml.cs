﻿using System;
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
        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void Login_Button(object sender, RoutedEventArgs e)
        {
            CheckPassword(master.Password);
        }
        // Provizorni kontrola hesla
        private void CheckPassword(string password)
        {
            MasterPassword masterObject = new MasterPassword();
            Algorithms algObject = new Algorithms();
            string master = File.ReadAllText(algObject.Rot1_3());
            bool masterMatch = masterObject.VerifyMasterPassword(password, master);
            
            if (masterMatch == true)
            {
                
                AfterLogin afterLoginWindow = new AfterLogin();
                afterLoginWindow.Show();
                //afterLoginWindow.ChangeLabel("You have entered your password vault!!!");

                this.Close();
            } else
            {
                //label1.Content = "Wrong Password!!!";
                //label1.Background = new SolidColorBrush(Colors.Red);
                errorLbl.Visibility = Visibility.Visible;
                errorLbl.Content = "Wrong master password!";
                
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
