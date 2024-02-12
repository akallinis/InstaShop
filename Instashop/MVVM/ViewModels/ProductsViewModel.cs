using Instashop.Core;
using Instashop.Services.Interfaces;
using MediatR;
using System.Windows.Input;

namespace Instashop.MVVM.ViewModels;

public class ProductsViewModel : BaseViewModel
{
    #region properties
    #endregion

    public ProductsViewModel(IMediator mediator, INavigationService navService)
        : base(mediator, navService)
    {
    }

    #region commands
    public ICommand GoToSalesCommand { get => new RelayCommand(GoToSales); }
    private void GoToSales(object param)
    {
        NavService.NavigateTo<SalesViewModel>();
    }
    #endregion
}
