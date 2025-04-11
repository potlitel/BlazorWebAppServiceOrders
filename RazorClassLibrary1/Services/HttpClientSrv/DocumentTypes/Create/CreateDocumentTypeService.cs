using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using FSA.Management.Application.Features.Roles.Create;
using Mapster;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;
using RazorClassLibrary1.Helpers;

namespace RazorClassLibrary1.Services.HttpClientSrv.DocumentTypes.Create
{
    public interface ICreateDocumentTypeService
    {
        Task<Result<CreateDocumentTypeResponse>> Handle(DocumentTypeDto item);
    }

    internal class CreateDocumentTypeService : HttpClientServiceBase<CreateDocumentTypeRequest, Result<CreateDocumentTypeResponse>>, 
                                               ICreateDocumentTypeService
    {
        public CreateDocumentTypeService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result<CreateDocumentTypeResponse>> Handle(DocumentTypeDto item)
        {
            try
            {
                var request = item.Adapt<CreateDocumentTypeRequest>();
                HttpContent content = CreateContent(request);

                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/document-types";
                var result = await _httpClient.PostAsync(url, content);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return Result<CreateDocumentTypeResponse>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }
    }
}
