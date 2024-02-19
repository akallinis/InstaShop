using Instashop.MVVM.Models;
using Instashop.MVVM.Models.BindingTargets;
using Instashop.Services.Interfaces;
using Shared.Responses.Factories;
using Shared.Responses.Interfaces;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace Instashop.Services.Implementations;

public class ApiService : IApiService
{
    private readonly HttpClient _httpClient;
    private string? _token;

    public ApiService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public async Task<IDataResponse> LoginAsync(string username, string password)
    {
        try
        {
            var content = new StringContent(
                System.Text.Json.JsonSerializer.Serialize(
                new { username = username, password = password }),
                Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("https://instapos.azurewebsites.net/api/login", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<LoginData>(result);
                _token = data.Token;
                return data != null && !string.IsNullOrEmpty(data.Token) ? DataResponseFactory.CreateDataResult(data) : DataResponseFactory.CreateAuthFailedResult();
            }

            return DataResponseFactory.CreateAuthFailedResult();
        }
        catch (HttpRequestException httpEx)
        {
            return DataResponseFactory.CreateExceptionResult(new Exception("Access to internet failed"));
        }
        catch (Exception ex)
        {
            return DataResponseFactory.CreateExceptionResult(ex);
        }

    }

    public async Task<IDataResponse> GetProductDetails(int productId)
    {
        try
        {
            if (_token != null)
            {
                var content = new StringContent(
                    System.Text.Json.JsonSerializer.Serialize(
                    new { productId = productId }),
                    Encoding.UTF8, "application/json");

                SetupHttpClient();

                var response = await _httpClient.PostAsync("https://instapos.azurewebsites.net/api/fetch-product-details", content);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<ProductDetailsData>(result);

                    return data != null ? DataResponseFactory.CreateDataResult(data) : DataResponseFactory.CreateAuthFailedResult();
                }

                return DataResponseFactory.CreateAuthFailedResult();
            }

            throw new Exception("Token expired or not obtained properly");
        }
        catch (HttpRequestException httpEx)
        {
            return DataResponseFactory.CreateExceptionResult(new Exception("Access to internet failed"));
        }
        catch (Exception ex)
        {
            return DataResponseFactory.CreateExceptionResult(ex);
        }
    }

    public async Task<IDataResponse> GetProductsAsync()
    {
        try
        {
            if (_token != null)
            {
                SetupHttpClient();

                var response = await _httpClient.GetAsync("https://instapos.azurewebsites.net/api/fetch-products");

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    var data = JsonConvert.DeserializeObject<ProductsData>(result);

                    return data != null ? DataResponseFactory.CreateDataResult(data.Products) : DataResponseFactory.CreateAuthFailedResult();
                }

                return DataResponseFactory.CreateAuthFailedResult();
            }

            throw new Exception("Token expired or not obtained properly");
        }
        catch (HttpRequestException httpEx)
        {
            return DataResponseFactory.CreateExceptionResult(new Exception("Access to internet failed"));
        }
        catch (Exception ex)
        {
            return DataResponseFactory.CreateExceptionResult(ex);
        }
    }

    public async Task<IDataResponse> GetSales()
    {
        try
        {
            SetupHttpClient();

            var response = await _httpClient.GetAsync("https://instapos.azurewebsites.net/api/fetch-sales");

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                var data = JsonConvert.DeserializeObject<List<Sale>>(result);

                return data != null ? DataResponseFactory.CreateDataResult(data) : DataResponseFactory.CreateAuthFailedResult();
            }

            return DataResponseFactory.CreateAuthFailedResult();
        }
        catch (HttpRequestException httpEx)
        {
            return DataResponseFactory.CreateExceptionResult(new Exception("Access to internet failed"));
        }
        catch (Exception ex)
        {
            return DataResponseFactory.CreateExceptionResult(ex);
        }
    }

    #region helpers
    private void SetupHttpClient()
    {
        if (_httpClient.DefaultRequestHeaders.TryGetValues("assignment-session-token", out var headerValues))
        {
            if (headerValues.Any(v => v != _token))
            {
                _httpClient.DefaultRequestHeaders.Remove("assignment-session-token");
                _httpClient.DefaultRequestHeaders.Add("assignment-session-token", _token);
            }
        }
        else
        {
            _httpClient.DefaultRequestHeaders.Add("assignment-session-token", _token);
        }
    }
    #endregion

}
