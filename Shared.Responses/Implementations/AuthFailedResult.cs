using Shared.Responses.Interfaces;

namespace Shared.Responses.Implementations;

public class AuthFailedResult : IDataResponse
{
    public string? Message { get; set; }

    public AuthFailedResult(string? message = null)
    {
        Message = message;
    }
}
