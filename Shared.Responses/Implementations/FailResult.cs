using Shared.Responses.Interfaces;

namespace Shared.Responses.Implementations;

public class FailResult : IDataResponse
{
    public string? Message { get; set; }

    public FailResult(string? message = null)
    {
        Message = message;
    }
}
