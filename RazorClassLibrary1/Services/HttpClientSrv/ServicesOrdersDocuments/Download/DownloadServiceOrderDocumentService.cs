using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Text;
using System.Text.Json;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServicesOrdersDocuments.Download
{
    public interface IDownloadServiceOrderDocumentService
    {
        Task<string> Handle(string blobName);
    }
    internal class DownloadServiceOrderDocumentService : HttpClientServiceBase<DownloadServiceOrderDocumentRequest, Result>,
                                                         IDownloadServiceOrderDocumentService
    {
        public DownloadServiceOrderDocumentService(IHttpClientFactory httpClientFactory, 
                                                   IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<string> Handle(string blobName)
        {
            try
            {
                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var request = new DownloadServiceOrderDocumentRequest(blobName);
                var payload = JsonSerializer.Serialize(request);
                var content = new StringContent(payload, Encoding.UTF8, "application/json");

                var url = $"so/document/download";
                var result = await _httpClient.PostAsync(url, content);
                result.EnsureSuccessStatusCode();

                return result.Content.Headers.ContentDisposition!.FileName!;
            }
            catch (Exception ex)
            {
                Result.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
                return null!;
            }
        }
    }
}
