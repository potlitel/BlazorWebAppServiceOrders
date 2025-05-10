using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Services.HttpClientSrv.ServicesOrdersDocuments.Download;
using System.Text;
using System.Text.Json;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServicesOrdersDocuments.DownloadAsStream
{
    public interface IDownloadServiceOrderDocumentAsStreamService
    {
        Task<Stream> Handle(string blobName);
    }
    internal class DownloadServiceOrderDocumentAsStreamService : HttpClientServiceBase<DownloadServiceOrderDocumentAsStreamRequest, Result>,
                                                                 IDownloadServiceOrderDocumentAsStreamService
    {
        public DownloadServiceOrderDocumentAsStreamService(IHttpClientFactory httpClientFactory, 
                                                           IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Stream> Handle(string blobName)
        {
            try
            {
                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var request = new DownloadServiceOrderDocumentRequest(blobName);
                var payload = JsonSerializer.Serialize(request);
                var content = new StringContent(payload, Encoding.UTF8, "application/json");

                var url = $"so/document/downloadAsStream";
                var result = await _httpClient.PostAsync(url, content);

                //string? contentType = null;

                //if (result.Content.Headers.TryGetValues("Content-Type", out var values))
                //{
                //    contentType = values.FirstOrDefault();
                //}

                result.EnsureSuccessStatusCode();

                return result.Content.ReadAsStream();
            }
            catch (Exception ex)
            {
                Result.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
                return null!;
            }
        }
    }
}
