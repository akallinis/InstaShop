namespace Shared.Responses.Interfaces;

public interface IHandlerResponse<T>
{
    T? Data { get; set; }
    List<string> Messages { get; set; }
    List<IErrorResult> Errors { get; set; }
}