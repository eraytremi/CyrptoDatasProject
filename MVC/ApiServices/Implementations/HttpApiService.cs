using Microsoft.Net.Http.Headers;
using PurchasingSystem.Web.ApiServices.Interfaces;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace PurchasingSystem.Web.ApiServices.Implementations
{
    public class HttpApiService : IHttpApiService
    {
        private readonly IConfiguration _configuration;
        private readonly IHttpClientFactory _httpClientFactory;
        public HttpApiService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
        }

        public async Task<T> DeleteDataAsync<T>(string endPoint, string token = null)
        {
            var client = _httpClientFactory.CreateClient();
            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri($"{_configuration["ServiceUrl:BaseAddress"]}{endPoint}"),
                Headers = { { HeaderNames.Accept, "application/json" } }
            };

            if (!string.IsNullOrEmpty(token))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var responseMessage = await client.SendAsync(requestMessage);
            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<T>(jsonResponse,
                new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return response;
        }

        public async Task<T> GetDataAsync<T>(string endPoint, string token = null)
        {
            var client = _httpClientFactory.CreateClient();
            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri($"{_configuration["ServiceUrl:BaseAddress"]}{endPoint}"),
                Headers = { { HeaderNames.Accept, "application/json" } }
            };

            if (!string.IsNullOrEmpty(token))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var responseMessage = await client.SendAsync(requestMessage);
            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return response;
        }

        public async Task<T> PostDataAsync<T>(string endPoint, string jsonData, string token = null)
        {
            var client = _httpClientFactory.CreateClient();
            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri($"{_configuration["ServiceUrl:BaseAddress"]}{endPoint}"),
                Headers = { { HeaderNames.Accept, "application/json" } },
                Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
            };

            if (!string.IsNullOrEmpty(token))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var responseMessage = await client.SendAsync(requestMessage);
            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return response;
        }

        public async Task<T> PutDataAsync<T>(string endPoint, string jsonData, string token = null)
        {
            var client = _httpClientFactory.CreateClient();
            var requestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"{_configuration["ServiceUrl:BaseAddress"]}{endPoint}"),
                Headers = { { HeaderNames.Accept, "application/json" } },
                Content = new StringContent(jsonData, Encoding.UTF8, "application/json")
            };

            if (!string.IsNullOrEmpty(token))
            {
                requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }

            var responseMessage = await client.SendAsync(requestMessage);
            var jsonResponse = await responseMessage.Content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<T>(jsonResponse, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            return response;
        }
    }
}
