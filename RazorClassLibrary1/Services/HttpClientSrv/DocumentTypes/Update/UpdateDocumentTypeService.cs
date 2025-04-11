using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using Mapster;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;

namespace RazorClassLibrary1.Services.HttpClientSrv.DocumentTypes.Update
{
    public interface IUpdateDocumentTypeService
    {
        Task<Result> Handle(DocumentTypeDto item);
    }
    internal class UpdateDocumentTypeService : HttpClientServiceBase<UpdateDocumentTypeRequest, Result>, IUpdateDocumentTypeService
    {
        public UpdateDocumentTypeService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result> Handle(DocumentTypeDto item)
        {
            try
            {
                var request = item.Adapt<UpdateDocumentTypeRequest>();
                HttpContent content = CreateContent(request);

                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/document-types";
                var result = await _httpClient.PutAsync(url, content);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return Result.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }
    }
}
