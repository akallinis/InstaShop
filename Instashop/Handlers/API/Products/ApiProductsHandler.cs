using Instashop.MVVM.Models;
using Instashop.Services.Interfaces;
using MediatR;
using Shared.Responses.Factories;
using Shared.Responses.Implementations;
using Shared.Responses.Interfaces;

namespace Instashop.Handlers.API.Products;

public class ApiProductsHandler : IRequestHandler<ApiProductsRequest, ApiProductsResponse>
{
    private readonly IApiService _api;
    public ApiProductsHandler(IApiService api)
    {
        _api = api;
    }

    public async Task<ApiProductsResponse> Handle(ApiProductsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _api.GetProductsAsync();

            var response = HandlerResponseFactory.CreateFromSingle(result, r => (r as DataResult<List<Product>>)?.Data);
            if (response != null && response.Data != null)
            {
                return new ApiProductsResponse { Data = response.Data, Errors = response.Errors, Messages = response.Messages };
            }

            return new ApiProductsResponse { Errors = new List<IErrorResult> { new ExceptionResult(new Exception("Request non valid")) }, Messages = new List<string> { "Either username or password was not provided" } };
        }
        catch (Exception ex)
        {
            return new ApiProductsResponse() { Errors = new List<IErrorResult> { new ExceptionResult(ex) } };
        }
    }
}
