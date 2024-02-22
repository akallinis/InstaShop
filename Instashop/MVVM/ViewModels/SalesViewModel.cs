using Instashop.Core;
using Instashop.MVVM.Models;
using Instashop.Services.Interfaces;
using Microsoft.Win32;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Instashop.MVVM.ViewModels;

public class SalesViewModel : BaseViewModel
{
    private readonly IExportDataService _exportDataService;

    #region properties
    private bool _exportEnabled;

    public bool ExportEnabled
    {
        get => _exportEnabled;
        set
        {
            _exportEnabled = value;
            OnPropertyChanged();
        }
    }

    private ObservableCollection<Sale> _sales;
    public ObservableCollection<Sale> Sales
    {
        get => _sales;
        set
        {
            _sales = value;
            ExportEnabled = value != null && value.Count() > 0;
            OnPropertyChanged();
        }
    }
    #endregion

    public SalesViewModel(IStoreManager storeManager, INavigationService navService, IExportDataService exportDataService)
        : base(storeManager, navService)
    {
        _exportDataService = exportDataService;
    }

    #region commands
    public ICommand ExportDataCommand { get => new RelayCommand(ExportData); }
    private void ExportData(object param)
    {
        var filePath = ShowSaveFileDialog();

        if (!string.IsNullOrEmpty(filePath))
        {
            _exportDataService.ExportToExcel(Sales.ToList(), filePath);
        }
    }

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

    #region helpers
    private string ShowSaveFileDialog()
    {
        SaveFileDialog saveFileDialog = new SaveFileDialog();
        saveFileDialog.FileName = $"Instashop_Sales_{DateTime.Now.ToString("MM-dd-yyyy")}";
        saveFileDialog.DefaultExt = "xlsx";
        saveFileDialog.Filter = $"Text files (*.xlsx)|*.xlsx|All files (*.*)|*.*";

        bool? result = saveFileDialog.ShowDialog();

        if (result == true)
        {
            return saveFileDialog.FileName;
        }
        else
        {
            return null;
        }
    }
    #endregion
}
