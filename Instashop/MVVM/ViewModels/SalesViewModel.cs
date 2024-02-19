using Instashop.Core;
using Instashop.MVVM.Models;
using Instashop.Services.Interfaces;
using MediatR;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Instashop.MVVM.ViewModels;

public class SalesViewModel : BaseViewModel
{
    #region properties
    private ObservableCollection<Sale> _sales;
    public ObservableCollection<Sale> Sales
    {
        get => _sales;
        set
        {
            _sales = value;
            OnPropertyChanged();
        }
    }
    #endregion

    public SalesViewModel(IStoreManager storeManager, INavigationService navService)
        : base(storeManager, navService)
    {
    }

    #region commands
    public ICommand GoToProductsCommand { get => new RelayCommand(GoToProducts); }
    private void GoToProducts(object param)
    {
        NavService.NavigateTo<ProductsViewModel>();
    }

    public ICommand LoadSalesCommand { get => new AsyncRelayCommand(async () => await LoadSales()); }
    private async Task LoadSales()
    {
        var response = await _storeManager.GetSalesAsync();

        Sales = response.Data != null ? new ObservableCollection<Sale>(response.Data) : new ObservableCollection<Sale>();
        HasBeenLoaded = true;
    }
    #endregion
}
