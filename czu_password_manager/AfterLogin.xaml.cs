using System;
using System.Collections.Generic;
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
    /// Interaction logic for AfterLogin.xaml
    /// </summary>
    public partial class AfterLogin : Window
    {
        public AfterLogin()
        {
            InitializeComponent();
        }

        public void ChangeLabel(string message)
        {
            label2.Content = message;
        }
    }
}
