using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace RazorClassLibrary1.Services
{
    public abstract class HttpClientServiceBase<TRequest, TResponse>
    {
        private readonly IHttpClientFactory httpClientFactory;
        private readonly IConfiguration configuration;

        protected HttpClientServiceBase(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            this.httpClientFactory = httpClientFactory;
            this.configuration = configuration;
        }

        public string HttpClientName(string httpClientName) => configuration[httpClientName]!;

        public async Task<HttpClient> GetWebApiAsync(string httpClientName)
        {
            var httpClient = httpClientFactory.CreateClient(httpClientName);
            return await Task.FromResult(httpClient);
        }

        public HttpContent CreateContent(TRequest item)
        {
            var payload = JsonSerializer.Serialize(item);
            return new StringContent(payload, Encoding.UTF8, "application/json");
        }

        public async Task<TResponse> GetResponseAsync(HttpContent content)
        {
            var stringContent = await content.ReadAsStringAsync();
            var response = JsonSerializer.Deserialize<TResponse>(stringContent);
            return response!;
        }
    }
}
