using Instashop.MVVM.Models;
using Instashop.Services.Interfaces;
using MediatR;
using Shared.Responses.Factories;
using Shared.Responses.Implementations;
using Shared.Responses.Interfaces;

namespace Instashop.Handlers.API.Products;

public class ApiSalesHandler : IRequestHandler<ApiSalesRequest, ApiSalesResponse>
{
    private readonly IApiService _api;
    public ApiSalesHandler(IApiService api)
    {
        _api = api;
    }

    public async Task<ApiSalesResponse> Handle(ApiSalesRequest request, CancellationToken cancellationToken)
    {
        try
        {
            var result = await _api.GetSales();
            var response = HandlerResponseFactory.CreateFromSingle(result, r => (r as DataResult<List<Sale>>)?.Data);
            return new ApiSalesResponse { Data = response.Data, Errors = response.Errors, Messages = response.Messages };
        }
        catch (Exception ex)
        {
            return new ApiSalesResponse() { Errors = new List<IErrorResult> { new ExceptionResult(ex) } };
        }
    }
}
