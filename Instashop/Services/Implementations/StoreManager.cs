using Instashop.Core;
using Instashop.Handlers.API.Login;
using Instashop.Handlers.API.Products;
using Instashop.Handlers.Repo.Products;
using Instashop.MVVM.Models;
using Instashop.Services.Interfaces;
using MediatR;

namespace Instashop.Services.Implementations;

public class StoreManager : IStoreManager
{
    private readonly IMediator _mediator;
    private readonly Timer _timer;
    private bool _firstTimerExecution = true;

    private List<Product> _products = new List<Product>();

    public StoreManager(IMediator mediator)
    {
        _mediator = mediator;
        _timer = new Timer(async (state) => await TimerCallbackAsync(state), null, TimeSpan.Zero, TimeSpan.FromMinutes(5));
    }

    public async Task<ApiProductDetailsResponse> GetProductDetailsAsync(int productId)
    {
        var request = new ApiProductDetailsRequest { ProductId = productId };
        return await _mediator.Send(request);
    }

    public async Task<ApiProductsResponse> GetProductsAsync()
    {

        if (_products.Any())
        {
            return new ApiProductsResponse { Data = _products };
        }

        var repoRequest = new RepoGetProductsRequest();
        var repoResponse = await _mediator.Send(repoRequest);

        if (repoResponse != null && repoResponse.Data != null)
        {
            _products = repoResponse.Data != null ? new List<Product>(repoResponse.Data) : new List<Product>();
        }

        if (!_products.Any())
        {
            var apiRequest = new ApiProductsRequest();
            var apiResponse = await _mediator.Send(apiRequest);

            _products = apiResponse.Data != null ? new List<Product>(apiResponse.Data) : new List<Product>();

            var repoSaveRequest = new RepoSaveProductsRequest { Products = _products };
            var repoSaveResponse = await _mediator.Send(repoSaveRequest);

            return new ApiProductsResponse { Data = _products, Errors = apiResponse.Errors, Messages = apiResponse.Messages };

        }

        return new ApiProductsResponse { Data = _products };

    }

    public async Task<ApiSalesResponse> GetSalesAsync()
    {
        var request = new ApiSalesRequest();
        return await _mediator.Send(request);
    }

    public async Task<ApiLoginResponse> LoginAsync(string username, string password)
    {
        var request = new ApiLoginRequest
        {
            Username = username,
            Password = password
        };

        return await _mediator.Send(request);
    }

    #region private helpers
    private async Task TimerCallbackAsync(object state)
    {
        if (_firstTimerExecution)
        {
            _firstTimerExecution = false;
            return;
        }

        var apiRequest = new ApiProductsRequest();
        var apiResponse = await _mediator.Send(apiRequest);

        if (apiResponse != null && apiResponse.Data != null)
        {
            List<Product> commonProducts = _products.Intersect(apiResponse.Data, new ProductComparer()).ToList();
            List<Product> newProducts = apiResponse.Data.Except(commonProducts, new ProductComparer()).ToList();

            List<Product> productsToUpdate = new List<Product>();
            if (commonProducts.Any())
            {
                foreach (var product in apiResponse.Data)
                {
                    if (!product.Equals(commonProducts.FirstOrDefault(pr => pr.Id == product.Id)))
                    {
                        productsToUpdate.Add(product);
                    }
                }
            }

            if (productsToUpdate.Any())
            {
                var request = new RepoUpdateProductsRequest { Products = productsToUpdate };
                var response = await _mediator.Send(request);
            }

            if (newProducts.Any())
            {
                var request = new RepoSaveProductsRequest { Products = newProducts };
                var response = await _mediator.Send(request);
            }


            var repoRequest = new RepoGetProductsRequest();
            var repoResponse = await _mediator.Send(repoRequest);

            _products = _products = repoResponse.Data != null ? new List<Product>(repoResponse.Data) : new List<Product>();

            if (productsToUpdate.Any() || newProducts.Any())
            {
                Messenger.SendProductListChangedMessage(new Messages.ProductListChangedMessage { Products = new List<Product>(_products) });
            }
        }
    }
    #endregion
}