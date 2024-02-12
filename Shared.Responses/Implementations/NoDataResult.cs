using Shared.Responses.Interfaces;

namespace Shared.Responses.Implementations;

public class NoDataResult : IDataResponse
{
    public string? Message { get; set; }

    public NoDataResult(string? message = null)
    {
        Message = message;
    }
}