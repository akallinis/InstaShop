using Instashop.MVVM.Models;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Instashop.Controls
{
    /// <summary>
    /// Interaction logic for SaleItemControl.xaml
    /// </summary>
    public partial class SaleItemControl : UserControl
    {
        public long SoldAt
        {
            get { return (long)GetValue(SoldAtProperty); }
            set { SetValue(SoldAtProperty, value); }
        }

        public static readonly DependencyProperty SoldAtProperty =
            DependencyProperty.Register("SoldAt", typeof(long), typeof(SaleItemControl), new PropertyMetadata());

        public double Total
        {
            get { return (double)GetValue(TotalProperty); }
            set { SetValue(TotalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Total.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalProperty =
            DependencyProperty.Register("Total", typeof(double), typeof(SaleItemControl), new PropertyMetadata());

        public List<SalesProduct> Products
        {
            get { return (List<SalesProduct>)GetValue(ProductsProperty); }
            set { SetValue(ProductsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Products.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProductsProperty =
            DependencyProperty.Register("Products", typeof(List<SalesProduct>), typeof(SaleItemControl), new PropertyMetadata(new List<SalesProduct>()));




        public SaleItemControl()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (DetailsPanel.Visibility == Visibility.Visible)
            {
                DetailsPanel.Visibility = Visibility.Hidden;
                BorderBox.Height = 50;
                ExpanderBtnIcon.Source = new BitmapImage(new Uri("/Instashop;component/Resources/Images/chevron-sign-down.png", UriKind.Relative));
            }
            else
            {
                DetailsPanel.Visibility = Visibility.Visible;
                BorderBox.Height = 50 + DetailsPanel.ActualHeight;
                ExpanderBtnIcon.Source = new BitmapImage(new Uri("/Instashop;component/Resources/Images/chevron-sign-up.png", UriKind.Relative));
            }
        }
    }
}
