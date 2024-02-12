using Instashop.Core;
using Instashop.Services.Interfaces;
using MediatR;

namespace Instashop.MVVM.ViewModels;

public class MainWindowViewModel : BaseViewModel
{
    public MainWindowViewModel(IMediator mediator, INavigationService navService)
        : base(mediator, navService)
    {
        //NavService.NavigateTo<LoginViewModel>();
    }
}
