using FSA.Core.DataTypes;
using FSA.Core.Utils;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;
using RazorClassLibrary1.Helpers;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServicesOrdersDocuments
{
    public interface IGetAllServicesOrdersDocumentsService
    {
        Task<ResultSO<ServiceOrderDocumentDto>> Handle(Pagination? pagination);
    }
    internal class GetAllServicesOrdersDocumentsService : HttpClientServiceBase<GetAllServicesOrdersDocumentsRequest, ResultSO<ServiceOrderDocumentDto>>,
                                                          IGetAllServicesOrdersDocumentsService
    {
        public GetAllServicesOrdersDocumentsService(IHttpClientFactory httpClientFactory, 
                                                    IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<ResultSO<ServiceOrderDocumentDto>> Handle(Pagination? pagination)
        {
            try
            {
                var request = new GetAllServicesOrdersDocumentsRequest(pagination);
                HttpContent content = CreateContent(request);

                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/documents/include-related-objects/all";
                var result = await _httpClient.PostAsync(url, content);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return ResultSO<ServiceOrderDocumentDto>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }
    }
}
