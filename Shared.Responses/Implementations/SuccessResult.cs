using Shared.Responses.Interfaces;

namespace Shared.Responses.Implementations;

public class SuccessResult : IDataResponse
{
    public string? Message { get; set; }

    public SuccessResult(string? message = null)
    {
        Message = message;
    }
}
