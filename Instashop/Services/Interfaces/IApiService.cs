using Shared.Responses.Interfaces;

namespace Instashop.Services.Interfaces;

public interface IApiService
{
    Task<IDataResponse> LoginAsync(string username, string password);
    Task<IDataResponse> GetProductsAsync();
    Task<IDataResponse> GetProductDetails(int productId);
    Task<IDataResponse> GetSales();
}
