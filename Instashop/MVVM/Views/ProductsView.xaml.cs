using Instashop.MVVM.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Instashop.MVVM.Views
{
    /// <summary>
    /// Interaction logic for ProductsView.xaml
    /// </summary>
    public partial class ProductsView : UserControl
    {
        public ProductsView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as ProductsViewModel;
            if (!vm.HasBeenLoaded)
            {
                vm?.LoadProductsCommand.Execute(vm);
            }
        }
    }
}
