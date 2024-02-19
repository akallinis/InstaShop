using Instashop.Services.Interfaces;
using MediatR;

namespace Instashop.Core;

public class BaseViewModel : ObservableObject
{
    protected readonly IStoreManager _storeManager;
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

    private bool _hasBeenLoaded;
    public bool HasBeenLoaded
    {
        get => _hasBeenLoaded;
        set
        {
            _hasBeenLoaded = value;
            OnPropertyChanged();
        }
    }

    public BaseViewModel(IStoreManager storeManager, INavigationService navService)
    {
        _storeManager = storeManager;
        NavService = navService;
    }
}