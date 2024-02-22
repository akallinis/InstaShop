using Instashop.Core;
using Instashop.Services.Interfaces;

namespace Instashop.MVVM.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    public MainWindowViewModel(IStoreManager storeManager, INavigationService navService)
        : base(storeManager, navService)
    {
        NavService.NavigateTo<LoginViewModel>();
    }
}
