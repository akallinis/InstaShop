using MediatR;

namespace Instashop.Handlers.API.Login;

public class ApiLoginRequest : IRequest<ApiLoginResponse>
{
    public string? Username { get; set; }
    public string? Password { get; set; }
}