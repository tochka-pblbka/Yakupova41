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
    /// Логика взаимодействия для ProductPage.xaml
    /// </summary>
    public partial class ProductPage : Page
    {
        public ProductPage(User user)
        {
            InitializeComponent();

         
            if (user.UserRole == 0) 
            {
                fioTB.Text = "Гость";
                RoleTB.Text = "Гость";

               
            }
            else
            {
                fioTB.Text = user.UserSurname + " " + user.UserName + " " + user.UserPatronymic;
                switch (user.UserRole)
                {
                    case 1:
                        RoleTB.Text = "Клиент"; break;
                    case 2:
                        RoleTB.Text = "Менеджер"; break;
                    case 3:
                        RoleTB.Text = "Администратор"; break;
                }
            }

            var currentProducts = Yakupova41Entities.GetContext().Product.ToList();
            ProductListView.ItemsSource = currentProducts;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEdit());
        }
}

        private void TBSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateService();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateService();
        }

        private void RButtonDown_Checked(object sender, RoutedEventArgs e)
        {
            UpdateService();
        }

        private void RButtonUp_Checked(object sender, RoutedEventArgs e)
        {
            UpdateService();
        }
        private void UpdateService()
        {

            var currentProducts = Yakupova41Entities.GetContext().Product.ToList();
            var raw_products_count = currentProducts.Count;

            if (TBSearch.Text.Length > 0)
                currentProducts = currentProducts.Where(p => p.ProductName.ToLower().Contains(TBSearch.Text.ToLower())).ToList();

            if (ComboType.SelectedIndex == 0)
            {
                currentProducts = currentProducts.Where(p => (Convert.ToInt32(p.ProductDiscountAmount) >= 0 && (Convert.ToInt32(p.ProductDiscountAmount)) <= 100)).ToList();
            }
            if (ComboType.SelectedIndex == 1)
            {
                currentProducts = currentProducts.Where(p => (Convert.ToInt32(p.ProductDiscountAmount) > 0 && (Convert.ToInt32(p.ProductDiscountAmount)) <= 9.99)).ToList();
            }
            if (ComboType.SelectedIndex == 2)
            {
                currentProducts = currentProducts.Where(p => (Convert.ToInt32(p.ProductDiscountAmount) > 9.99 && (Convert.ToInt32(p.ProductDiscountAmount)) <= 14.99)).ToList();
            }
            if (ComboType.SelectedIndex == 3)
            {
                currentProducts = currentProducts.Where(p => (Convert.ToInt32(p.ProductDiscountAmount) > 15 && (Convert.ToInt32(p.ProductDiscountAmount)) <= 100)).ToList();
            }

            currentProducts = currentProducts.Where(p => p.ProductName.ToLower().Contains(TBSearch.Text.ToLower())).ToList();
            SearchResultTB.Text = "кол-во " + Convert.ToString(currentProducts.Count) + " из " + Convert.ToString(raw_products_count);
            
            ProductListView.ItemsSource = currentProducts;
            ProductListView.ItemsSource = currentProducts.ToList();

            if (RButtonDown.IsChecked.Value)
            {
                ProductListView.ItemsSource = currentProducts.OrderByDescending(p => p.ProductCost).ToList();
            }
            if (RButtonUp.IsChecked.Value)
            {
                ProductListView.ItemsSource = currentProducts.OrderBy(p => p.ProductCost).ToList();
            }

        }
    }
}
