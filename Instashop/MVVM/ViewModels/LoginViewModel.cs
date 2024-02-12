using Instashop.Core;
using Instashop.Handlers.API.Login;
using Instashop.Services.Interfaces;
using MediatR;
using System.Windows;
using System.Windows.Input;

namespace Instashop.MVVM.ViewModels;

public class LoginViewModel : BaseViewModel
{

    #region properties
    private string? _username;
    public string Username
    {
        get { return _username ?? string.Empty; }
        set
        {
            _username = value;
            OnPropertyChanged();
        }
    }

    private string? _password;
    public string Password
    {
        get { return _password ?? string.Empty; }
        set
        {
            _password = value;
            OnPropertyChanged();
        }
    }
    #endregion

    public LoginViewModel(IMediator mediator, INavigationService navService)
        : base(mediator, navService)
    {
    }

    #region commands
    public ICommand LoginCommand { get => new AsyncRelayCommand(async () => await LoginAsync()); }
    private async Task LoginAsync()
    {
        var request = new ApiLoginRequest
        {
            Username = Username,
            Password = Password,
        };

        var response = await _mediator.Send(request);

        if (response.Errors.Any())
        {
            string messageBoxText = response.Errors?.FirstOrDefault()?.Message ?? "Authentication error";
            string caption = "Failure";
            MessageBoxButton button = MessageBoxButton.OK;
            MessageBoxImage icon = MessageBoxImage.Error;
            MessageBoxResult result;

            result = MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);
            return;
        }

        Console.WriteLine("We should navigate to next pages");
        NavService.NavigateTo<ProductsViewModel>();
    }
    #endregion

}
