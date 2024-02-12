using Shared.Responses.Interfaces;

namespace Shared.Responses.Implementations;

public class DataResult<T> : IDataResponse
{
    public string? Message { get; set; }
    public T Data { get; }

    public DataResult(T data, string? message = null)
    {
        Data = data;
        Message = message;
    }
}