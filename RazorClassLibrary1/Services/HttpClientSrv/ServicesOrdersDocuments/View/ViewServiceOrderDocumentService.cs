using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServicesOrdersDocuments.View
{
    public interface IViewServiceOrderDocumentService
    {
        Task<string> Handle(string blobName);
    }

    internal class ViewServiceOrderDocumentService : HttpClientServiceBase<ViewServiceOrderDocumentRequest, Result>,
                                                     IViewServiceOrderDocumentService
    {
        public ViewServiceOrderDocumentService(IHttpClientFactory httpClientFactory, 
                                               IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<string> Handle(string blobName)
        {
            try
            {
                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var request = new ViewServiceOrderDocumentRequest(blobName);
                var payload = JsonSerializer.Serialize(request);
                var content = new StringContent(payload, Encoding.UTF8, "application/json");

                var url = $"so/document/view";
                var result = await _httpClient.PostAsync(url, content);
                result.EnsureSuccessStatusCode();

                var urlDoc = await result.Content.ReadAsStringAsync();
                return urlDoc;
            }
            catch (Exception ex)
            {
                Result.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
                return null!;
            }
        }
    }
}
