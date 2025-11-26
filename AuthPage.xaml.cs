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
        private int failedAttempts = 0;
        private string currentCaptcha = "";
        public AuthPage()
        {
            InitializeComponent();
        }

        private void LikeGuest_Click(object sender, RoutedEventArgs e)
        {

            Manager.MainFrame.Navigate(new ProductPage(null));
        }

        private void GenerateCaptcha()
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            currentCaptcha = new string(Enumerable.Repeat(chars, 4)
                .Select(s => s[random.Next(s.Length)]).ToArray());

            captchaOneWord.Text = currentCaptcha[0].ToString();
            captchaTwoWord.Text = currentCaptcha[1].ToString();
            captchaThreeWord.Text = currentCaptcha[2].ToString();
            captchaFourWord.Text = currentCaptcha[3].ToString();

            CaptchaPanel.Visibility = Visibility.Visible;
            CaptchaTB.Visibility = Visibility.Visible;
            CaptchaTB.Text = "";
        }



        private async void Enter_Click(object sender, RoutedEventArgs e)
        {
            string login = LoginTB.Text;
            string password = PassTB.Text;
            if (login == "" || password == "")
            {
                MessageBox.Show("Есть пустые поля");
                return;
            }

            if (failedAttempts > 0)
            {
                if (CaptchaTB.Text != currentCaptcha)
                {
                    MessageBox.Show("Неверная капча");
                    GenerateCaptcha();
                    return;
                }
            }


            User user = Yakupova41Entities.GetContext().User.ToList().Find(p => p.UserLogin == login && p.UserPassword == password);
            if (user != null)
            {
                Manager.MainFrame.Navigate(new ProductPage(user));
                LoginTB.Text = "";
                PassTB.Text = "";
                failedAttempts = 0;
                CaptchaPanel.Visibility = Visibility.Collapsed;
                CaptchaTB.Visibility = Visibility.Collapsed;
            }
            else
            {
                failedAttempts++;
                MessageBox.Show("Введены неверные данные");
                if (failedAttempts == 1)
                {
                    GenerateCaptcha();
                }
                else if (failedAttempts > 1)
                {
                    Enter.IsEnabled = false;
                    await Task.Delay(10000);
                    Enter.IsEnabled = true;
                    GenerateCaptcha();
                }
            }

        }
    }
}
