using Instashop.Services.Interfaces;
using MediatR;
using Shared.Responses.Implementations;
using Shared.Responses.Interfaces;

namespace Instashop.Handlers.Repo.Products;

public class RepoUpdateProductsHandler : IRequestHandler<RepoUpdateProductsRequest, RepoUpdateProductsResponse>
{
    private readonly IProductsRepository _productsRepository;
    public RepoUpdateProductsHandler(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public async Task<RepoUpdateProductsResponse> Handle(RepoUpdateProductsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Products != null && request.Products.Any())
            {
                var result = _productsRepository.UpdateProducts(request.Products);

                switch (result)
                {
                    case SuccessResult:
                        return new RepoUpdateProductsResponse() { Messages = new List<string> { "Save successfull" } };
                    case FailResult failResult:
                        return new RepoUpdateProductsResponse() { Errors = new List<IErrorResult> { new ExceptionResult(new Exception()) }, Messages = new List<string> { failResult.Message } };
                    case ExceptionResult exceptionResult:
                        return new RepoUpdateProductsResponse() { Errors = new List<IErrorResult> { exceptionResult }, Messages = new List<string> { exceptionResult.Message } };
                }
            }
            return new RepoUpdateProductsResponse();
        }
        catch (Exception ex)
        {
            return new RepoUpdateProductsResponse() { Errors = new List<IErrorResult> { new ExceptionResult(ex) } };
        }
    }
}
