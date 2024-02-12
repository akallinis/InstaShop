using Instashop.MVVM.Models.BindingTargets;
using Instashop.Services.Interfaces;
using Shared.Responses.Factories;
using Shared.Responses.Interfaces;
using System.Net.Http;
using System.Text;

namespace Instashop.Services.Implementations;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private string? _token;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<IDataResponse> LoginAsync(string username, string password)
    {
        try
        {
            var content = new StringContent(
            System.Text.Json.JsonSerializer.Serialize(
                new { username = username, password = password }),
            Encoding.UTF8, "application/json");

            _httpClient.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            var response = await _httpClient.PostAsync("https://instapos.azurewebsites.net/api/login", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var data = Newtonsoft.Json.JsonConvert.DeserializeObject<LoginData>(result);

                return data != null && !string.IsNullOrEmpty(data.Token) ? DataResponseFactory.CreateDataResult(data) : DataResponseFactory.CreateAuthFailedResult();
            }

            return DataResponseFactory.CreateAuthFailedResult();
        }
        catch (Exception ex)
        {
            return DataResponseFactory.CreateExceptionResult(ex);
        }

    }

    Task<IDataResponse> IApiService.GetProductDetails(int productId)
    {
        throw new NotImplementedException();
    }

    Task<IDataResponse> IApiService.GetProductsAsync()
    {
        throw new NotImplementedException();
    }

    Task<IDataResponse> IApiService.GetSales()
    {
        throw new NotImplementedException();
    }

    private bool IsLoggedIn()
    {
        return _token != null;
    }

}
