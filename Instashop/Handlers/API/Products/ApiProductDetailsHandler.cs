using Instashop.MVVM.Models.BindingTargets;
using Instashop.Services.Interfaces;
using MediatR;
using Shared.Responses.Factories;
using Shared.Responses.Implementations;
using Shared.Responses.Interfaces;

namespace Instashop.Handlers.API.Products;

public class ApiProductDetailsHandler : IRequestHandler<ApiProductDetailsRequest, ApiProductDetailsResponse>
{
    private readonly IApiService _api;
    public ApiProductDetailsHandler(IApiService api)
    {
        _api = api;
    }

    public async Task<ApiProductDetailsResponse> Handle(ApiProductDetailsRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _api.GetProductDetails(request.ProductId);

            var response = HandlerResponseFactory.CreateFromSingle(result, r => (r as DataResult<ProductDetailsData>)?.Data);
            if (response != null)
            {
                return new ApiProductDetailsResponse { Data = response.Data, Errors = response.Errors, Messages = response.Messages };
            }

            return new ApiProductDetailsResponse { Errors = new List<IErrorResult> { new ExceptionResult(new Exception("No data found")) }, Messages = new List<string> { $"No details found for ProductId: {request.ProductId}" } };
        }
        catch (Exception ex)
        {
            return new ApiProductDetailsResponse { Errors = new List<IErrorResult> { new ExceptionResult(ex) } };
        }
    }
}
