using Instashop.Core;
using Instashop.Services.Interfaces;
using MediatR;
using System.Windows.Input;

namespace Instashop.MVVM.ViewModels;

public class SalesViewModel : BaseViewModel
{
    #region properties
    #endregion

    public SalesViewModel(IMediator mediator, INavigationService navService)
        : base(mediator, navService)
    {
    }

    #region commands
    public ICommand GoToProductsCommand { get => new RelayCommand(GoToProducts); }
    private void GoToProducts(object param)
    {
        NavService.NavigateTo<ProductsViewModel>();
    }
    #endregion
}
