using Shared.Responses.Interfaces;

namespace Shared.Responses.Implementations;

public class NoDataExpectedResult : IDataResponse
{
    public string? Message { get; set; }

    public NoDataExpectedResult(string? message = null)
    {
        Message = message;
    }
}