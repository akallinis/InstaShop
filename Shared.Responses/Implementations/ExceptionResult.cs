using System.Text.Json.Serialization;

using Shared.Responses.Interfaces;

namespace Shared.Responses.Implementations;

public class ExceptionResult : IErrorResult, IDataResponse
{
    public string? Message { get; set; }
    [JsonIgnore] public Exception Exception { get; set; }

    public ExceptionResult(Exception exception)
    {
        Exception = exception;
        Message = exception.Message;
    }
}