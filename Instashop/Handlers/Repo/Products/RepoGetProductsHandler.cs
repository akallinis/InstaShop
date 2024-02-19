using Instashop.MVVM.Models;
using Instashop.Services.Interfaces;
using MediatR;
using Shared.Responses.Factories;
using Shared.Responses.Implementations;
using Shared.Responses.Interfaces;

namespace Instashop.Handlers.Repo.Products;

public class RepoGetProductsHandler : IRequestHandler<RepoGetProductsRequest, RepoGetProductsResponse>
{
    private readonly IProductsRepository _productsRepository;
    public RepoGetProductsHandler(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public Task<RepoGetProductsResponse> Handle(RepoGetProductsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = _productsRepository.GetProductsAll();
            var response = HandlerResponseFactory.CreateFromSingle(result, r => (r as DataResult<List<Product>>)?.Data);
            if (response != null && response.Data != null)
            {
                return Task.FromResult(new RepoGetProductsResponse { Data = response.Data, Errors = response.Errors, Messages = response.Messages });
            }

            return Task.FromResult(new RepoGetProductsResponse { Errors = new List<IErrorResult> { new ExceptionResult(new Exception("No data found")) }, Messages = new List<string> { "No data saved on local database" } });
        }
        catch (Exception ex)
        {
            return Task.FromResult(new RepoGetProductsResponse() { Errors = new List<IErrorResult> { new ExceptionResult(ex) } });
        }
    }
}
