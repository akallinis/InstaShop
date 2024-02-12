using Shared.Responses.Implementations;

namespace Shared.Responses.Factories;

public static class DataResponseFactory
{
    public static DataResult<T> CreateDataResult<T>(T data)
    {
        return new DataResult<T>(data);
    }

    public static NoDataResult CreateNoDataResult(string? message = "No data were found for the provided parameters.")
    {
        return new NoDataResult(message);
    }

    public static NoDataExpectedResult CreateNoDataExpectedResult(string? message = "No data were expected to be returned.")
    {
        return new NoDataExpectedResult(message);
    }

    public static AuthFailedResult CreateAuthFailedResult(string? message = "Authentication failed.")
    {
        return new AuthFailedResult(message);
    }

    public static ExceptionResult CreateExceptionResult(Exception exception)
    {
        return new ExceptionResult(exception);
    }
}