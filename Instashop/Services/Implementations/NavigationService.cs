using Instashop.Core;
using Instashop.Services.Interfaces;

namespace Instashop.Services.Implementations;

public class NavigationService : ObservableObject, INavigationService
{
    private BaseViewModel _currentView;
    private readonly Func<Type, BaseViewModel> _viewModelFactory;

    public BaseViewModel CurrentView
    {
        get => _currentView;
        private set
        {
            _currentView = value;
            OnPropertyChanged();
        }
    }

    public NavigationService(Func<Type, BaseViewModel> viewModelFactory)
    {
        _viewModelFactory = viewModelFactory;
    }

    public void NavigateTo<T>() where T : BaseViewModel
    {
        var viewModel = _viewModelFactory.Invoke(typeof(T));
        CurrentView = viewModel;
    }
}