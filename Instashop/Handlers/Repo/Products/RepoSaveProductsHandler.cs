using Instashop.Services.Interfaces;
using MediatR;
using Shared.Responses.Implementations;
using Shared.Responses.Interfaces;

namespace Instashop.Handlers.Repo.Products;

public class RepoSaveProductsHandler : IRequestHandler<RepoSaveProductsRequest, RepoSaveProductsResponse>
{
    private readonly IProductsRepository _productsRepository;
    public RepoSaveProductsHandler(IProductsRepository productsRepository)
    {
        _productsRepository = productsRepository;
    }

    public async Task<RepoSaveProductsResponse> Handle(RepoSaveProductsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (request.Products != null && request.Products.Any())
            {
                var result = _productsRepository.SaveProducts(request.Products);

                switch (result)
                {
                    case SuccessResult:
                        return new RepoSaveProductsResponse() { Messages = new List<string> { "Save successfull" } };
                    case FailResult failResult:
                        return new RepoSaveProductsResponse() { Errors = new List<IErrorResult> { new ExceptionResult(new Exception()) }, Messages = new List<string> { failResult.Message } };
                    case ExceptionResult exceptionResult:
                        return new RepoSaveProductsResponse() { Errors = new List<IErrorResult> { exceptionResult }, Messages = new List<string> { exceptionResult.Message } };
                }
            }
            return new RepoSaveProductsResponse();
        }
        catch (Exception ex)
        {
            return new RepoSaveProductsResponse() { Errors = new List<IErrorResult> { new ExceptionResult(ex) } };
        }
    }
}
