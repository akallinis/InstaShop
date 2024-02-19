using Instashop.MVVM.Models.BindingTargets;
using Instashop.Services.Interfaces;
using MediatR;
using Shared.Responses.Factories;
using Shared.Responses.Implementations;
using Shared.Responses.Interfaces;

namespace Instashop.Handlers.API.Login;

public class ApiLoginHandler : IRequestHandler<ApiLoginRequest, ApiLoginResponse>
{
    private readonly IApiService _api;

    public ApiLoginHandler(IApiService api)
    {
        _api = api;
    }

    public async Task<ApiLoginResponse> Handle(ApiLoginRequest request, CancellationToken cancellationToken)
    {
        try
        {
            if (!string.IsNullOrEmpty(request.Username) && !string.IsNullOrEmpty(request.Password))
            {
                var result = await _api.LoginAsync(request.Username, request.Password);

                var response = HandlerResponseFactory.CreateFromSingle(result, r => (r as DataResult<LoginData>)?.Data);

                return new ApiLoginResponse { Data = response.Data, Errors = response.Errors, Messages = response.Messages };
            }

            return new ApiLoginResponse { Errors = new List<IErrorResult> { new ExceptionResult(new Exception("Request non valid")) }, Messages = new List<string> { "Either username or password was not provided" } };
        }
        catch (Exception ex)
        {
            return new ApiLoginResponse() { Errors = new List<IErrorResult> { new ExceptionResult(ex) } };
        }
    }
}