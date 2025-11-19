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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Yakupova41
{
    /// <summary>
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        public AuthPage()
        {
            InitializeComponent();
        }

        private void LikeGuest_Click(object sender, RoutedEventArgs e)
        {
           
            User guestUser = new User
            {
                UserID = 0,
                UserSurname = "Гость",
                UserName = "",
                UserPatronymic = "",
                UserRole = 0
            };

          
            Manager.MainFrame.Navigate(new ProductPage(guestUser));

            
            LoginTB.Text = "";
            PassTB.Text = "";
        }

        private void Enter_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTB.Text;
            string password = PassTB.Text;
            if (login == "" || password == "")
            {
                MessageBox.Show("Есть пустые поля");
                return;
            }

            User user = Yakupova41Entities.GetContext().User.ToList().Find(p => p.UserLogin == login && p.UserPassword == password);
            if (user != null)
            {
                Manager.MainFrame.Navigate(new ProductPage(user));
                LoginTB.Text = "";
                PassTB.Text = "";
            }
            else
            {
                MessageBox.Show("Введены неверные данные");
                LoginTB.IsEnabled = false;

                Enter.IsEnabled = true;
            }

        }
    }
}
