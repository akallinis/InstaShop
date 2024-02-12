using Shared.Responses.Implementations;
using Shared.Responses.Interfaces;

namespace Shared.Responses.Factories;

public static class HandlerResponseFactory
{
    public static HandlerResponse<T> CreateFromSingle<T>(IDataResponse result, Func<IDataResponse, T> dataMapper)
    {
        var response = new HandlerResponse<T>();

        switch (result)
        {
            case DataResult<T> dataResult:
                response.Data = dataMapper(dataResult);
                break;
            case NoDataExpectedResult noDataExpectedResult:
                if (!string.IsNullOrWhiteSpace(noDataExpectedResult.Message))
                {
                    response.Messages.Add(noDataExpectedResult.Message);
                }
                break;
            case NoDataResult noDataResult:
                if (!string.IsNullOrWhiteSpace(noDataResult.Message))
                {
                    response.Messages.Add(noDataResult.Message);
                }
                break;
            case AuthFailedResult authFailedResult:
                response.Errors.AddRange(new List<IErrorResult> { new ExceptionResult(new Exception(authFailedResult.Message)) });
                break;
            case IErrorResult errorResult:
                response.Errors.Add(errorResult);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(result));
        }

        return response;
    }

    public static HandlerResponse<T> CreateFromMultiple<T>(IEnumerable<IDataResponse> results, Func<IEnumerable<IDataResponse>, T> dataMapper)
    {
        var response = new HandlerResponse<T>();
        var repositoryResponses = results.ToList();

        var messages = repositoryResponses.Where(r => !string.IsNullOrWhiteSpace(r.Message)).Select(r => r.Message).ToList();

        if (messages.Any())
        {
            response.Messages.AddRange(messages!);
        }

        var errorResults = repositoryResponses.OfType<IErrorResult>().ToList();
        if (errorResults.Any())
        {
            response.Errors.AddRange(errorResults);
        }
        else
        {
            response.Data = dataMapper(repositoryResponses);
        }

        return response;
    }
}
