using Instashop.Core;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using System.Reflection;
using Instashop.Services.Interfaces;
using Instashop.Services.Implementations;
using System.Net.Http;
using Instashop.MVVM.ViewModels;
using Instashop.MVVM.Views;

namespace Instashop;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{
    private readonly ServiceProvider _serviceProvider;

    public App()
    {
        //Configure services
        var services = new ServiceCollection();

        ConfigureServices(services);

        _serviceProvider = services.BuildServiceProvider();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton<MainWindow>(provider => new MainWindow
        {
            DataContext = provider.GetRequiredService<MainWindowViewModel>()
        });
        services.AddSingleton<MainWindowViewModel>();
        services.AddSingleton<LoginViewModel>();
        services.AddSingleton<ProductsViewModel>();
        services.AddSingleton<SalesViewModel>();


        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        services.AddDbContext<InstaDbContext>();
        services.AddSingleton<IApiService, ApiService>();//call external api service
        services.AddSingleton<IProductsRepository, ProductsRepository>();//local postgre db service
        services.AddSingleton<INavigationService, NavigationService>();
        services.AddSingleton<HttpClient>();
        services.AddSingleton<IStoreManager, StoreManager>();//wrapper service
        services.AddSingleton<Func<Type, BaseViewModel>>(provider => baseViewModelType => (BaseViewModel)provider.GetRequiredService(baseViewModelType));
    }

    protected override void OnStartup(StartupEventArgs e)
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
        mainWindow.Show();
        base.OnStartup(e);
    }
}
