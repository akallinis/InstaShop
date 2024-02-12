using Shared.Responses.Interfaces;

namespace Shared.Responses.Implementations;

public class HandlerResponse<T> : IHandlerResponse<T>
{
    public T? Data { get; set; }
    public List<string> Messages { get; set; } = new();
    public List<IErrorResult> Errors { get; set; } = new();
}