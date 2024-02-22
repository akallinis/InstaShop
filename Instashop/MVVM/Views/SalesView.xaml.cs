using Instashop.MVVM.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Instashop.MVVM.Views
{
    /// <summary>
    /// Interaction logic for SalesView.xaml
    /// </summary>
    public partial class SalesView : UserControl
    {
        public SalesView()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            var vm = this.DataContext as SalesViewModel;
            if (!vm.HasBeenLoaded)
            {
                vm?.LoadSalesCommand.Execute(vm);
            }
        }
    }
}
