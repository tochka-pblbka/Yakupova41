using System;
using System.Collections.Generic;
using System.Data;
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

namespace Yakupova41
{
    /// <summary>
    /// Логика взаимодействия для OrderWindow.xaml
    /// </summary>
    public partial class OrderWindow : Window
    {
        List<OrderProduct> selectedOrderProducts = new List<OrderProduct>();
        List<Product> selectedProducts = new List<Product>();

        private Order currentOrder = new Order();
        private OrderProduct currentOrderProduct = new OrderProduct();

        public OrderWindow(List<OrderProduct> selectedOrderProducts, List<Product> selectedProducts, string FIO)
        {
            InitializeComponent();
            var currentPickups = Yakupova41Entities.GetContext().PickUpPoint.ToList();
            PickupCombo.ItemsSource = currentPickups;
            ClientFIO.Text = FIO;
            OrderNumber.Text = selectedOrderProducts.First().OrderID.ToString();

            ProductListView.ItemsSource = selectedProducts;
            foreach (Product p in selectedProducts)
            {
                p.ProductQuantityInStock = 1;
                foreach(OrderProduct q in selectedOrderProducts)
                {
                    if (p.ProductArticleNumber == q.ProductArticleNumber)
                        p.ProductQuantityInStock = q.OrderProductCount;
                }

            }
            this.selectedOrderProducts = selectedOrderProducts; 
            this.selectedProducts = selectedProducts;
            StartDate.Text = DateTime.Now.ToString();
            SetDeliveryDate();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Yakupova41Entities.GetContext().SaveChanges();
                MessageBox.Show("информация сохранена");
                Manager.MainFrame.GoBack();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }
    }
}
