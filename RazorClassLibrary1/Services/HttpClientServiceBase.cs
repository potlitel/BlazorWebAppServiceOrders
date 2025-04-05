using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Services.HttpClientSrv.DocumentTypes.GetAll;
using System.Text;
using System.Text.Json;
//using System.Text.Json;

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

            TResponse? response;
            try
            {
                //response = JsonSerializer.Deserialize<TResponse>(stringContent);
                response = Newtonsoft.Json.JsonConvert.DeserializeObject<TResponse>(stringContent);
            }
            catch (Exception ex)
            {
                throw ex!;
            }

            return response!;
        }
    }
}
