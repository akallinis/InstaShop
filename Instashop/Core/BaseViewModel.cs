using Instashop.Services.Interfaces;
using MediatR;

namespace Instashop.Core;

public class BaseViewModel : ObservableObject
{
    protected readonly IMediator _mediator;
    private INavigationService _navService;
    public INavigationService NavService
    {
        get => _navService;
        set
        {
            _navService = value;
            OnPropertyChanged();
        }
    }

    public BaseViewModel(IMediator mediator, INavigationService navService)
    {
        _mediator = mediator;
        NavService = navService;
    }
}