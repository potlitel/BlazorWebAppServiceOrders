using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using Mapster;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;
using RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderRegisters.Create;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServicesOrdersDocuments.Create
{
    public interface ICreateServiceOrderDocumentService
    {
        Task<Result<CreateServiceOrderDocumentResponse>> Handle(ServiceOrderDocumentDto item);
    }
    internal class CreateServiceOrderDocumentService : HttpClientServiceBase<CreateServiceOrderDocumentRequest, Result<CreateServiceOrderDocumentResponse>>,
                                                       ICreateServiceOrderDocumentService
    {
        public CreateServiceOrderDocumentService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result<CreateServiceOrderDocumentResponse>> Handle(ServiceOrderDocumentDto item)
        {
            try
            {
                var request = item.Adapt<CreateServiceOrderDocumentRequest>();
                HttpContent content = CreateContent(request);

                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/registers";
                var result = await _httpClient.PostAsync(url, content);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return Result<CreateServiceOrderDocumentResponse>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }
    }
}
