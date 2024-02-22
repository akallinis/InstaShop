using Instashop.Core;
using Instashop.MVVM.Models;
using Instashop.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Instashop.MVVM.ViewModels;

public class ProductsViewModel : BaseViewModel
{
    #region properties
    private ObservableCollection<Product> _products;
    public ObservableCollection<Product> Products
    {
        get => _products;
        set
        {
            _products = value;
            OnPropertyChanged();
        }
    }

    private Product? _selectedProduct;
    public Product? SelectedProduct
    {
        get => _selectedProduct;
        set
        {
            _selectedProduct = value;
            OnPropertyChanged();

            
            if (_selectedProduct != null)
                Task.Run(async () => await GetDetailsForProduct(_selectedProduct.Id));
        }
    }

    private string _detailsImage;
    public string DetailsImage
    {
        get => _detailsImage;
        set
        {
            _detailsImage = value;
            OnPropertyChanged();
        }
    }

    private string _detailsDescription;
    public string DetailsDescription
    {
        get => _detailsDescription;
        set
        {
            _detailsDescription = value;
            OnPropertyChanged();
        }
    }

    private string _detailsBrand;
    public string DetailsBrand
    {
        get => _detailsBrand;
        set
        {
            _detailsBrand = value;
            OnPropertyChanged();
        }
    }

    private string _detailsPackaging;
    public string DetailsPackaging
    {
        get => _detailsPackaging;
        set
        {
            _detailsPackaging = value;
            OnPropertyChanged();
        }
    }

    private int _detailsStock;
    public int DetailsStock
    {
        get => _detailsStock;
        set
        {
            _detailsStock = value;
            OnPropertyChanged();
        }
    }

    private bool _detailsVisible;
    public bool DetailsVisible
    {
        get => _detailsVisible;
        set
        {
            _detailsVisible = value;
            OnPropertyChanged();
        }
    }

    private string _clearSelectionButtonText;
    public string ClearSelectionButtonText
    {
        get => _clearSelectionButtonText;
        set
        {
            _clearSelectionButtonText = value;
            OnPropertyChanged();
        }
    }
    #endregion

    public ProductsViewModel(IStoreManager storeManager, INavigationService navService)
        : base(storeManager, navService)
    {
        ClearSelectionButtonText = "Clear ()";
        Messenger.ProductListChanged += _messenger_ProductListChanged;
    }

    private void _messenger_ProductListChanged(object? sender, Messages.ProductListChangedMessage e)
    {
        if (e.Products != null)
            Products = new ObservableCollection<Product>(e.Products);
    }

    #region commands
    public ICommand LoadProductsCommand { get => new AsyncRelayCommand(async () => await LoadProducts()); }
    private async Task LoadProducts()
    {
        var response = await _storeManager.GetProductsAsync();

        Products = response.Data != null ? new ObservableCollection<Product>(response.Data) : new ObservableCollection<Product>();
        HasBeenLoaded = true;
    }

    public ICommand GoToSalesCommand { get => new RelayCommand(GoToSales); }
    private void GoToSales(object param)
    {
        NavService.NavigateTo<SalesViewModel>();
    }

    public ICommand ClearSelectedProductCommand { get => new RelayCommand(ClearSelectedProduct); }
    private void ClearSelectedProduct(object obj)
    {
        SelectedProduct = null;
        DetailsVisible = false;
    }
    #endregion

    #region helpers
    private async Task GetDetailsForProduct(int productId)
    {
        var response = await _storeManager.GetProductDetailsAsync(productId);

        if (response == null || response.Data == null)
            return;

        ClearSelectionButtonText = _selectedProduct != null ? $"Clear ({_selectedProduct.Name})" : "Clear ()";
        DetailsImage = string.IsNullOrEmpty(response.Data.Image) ? string.Empty : response.Data.Image;
        DetailsDescription = string.IsNullOrEmpty(response.Data.Description) ? string.Empty : response.Data.Description;
        DetailsBrand = string.IsNullOrEmpty(response.Data.Brand) ? string.Empty : response.Data.Brand;
        DetailsPackaging = string.IsNullOrEmpty(response.Data.Packaging) ? string.Empty : response.Data.Packaging;
        DetailsStock = response.Data.Stock;
        DetailsVisible = SelectedProduct != null;
    }
    #endregion
}
