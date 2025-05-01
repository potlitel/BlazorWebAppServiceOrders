using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using FSA.Core.Utils;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServicesOrdersDocuments.GetAllBySOId
{
    public interface IGetServiceOrderDocumentsBySOIdService
    {
        Task<Result<IEnumerable<ServiceOrderDocumentDto>>> Handle(int id_SO, Pagination? Pagination);
    }
    internal class GetServiceOrderDocumentsBySOIdService : HttpClientServiceBase<GetServiceOrderDocumentsBySOIdRequest,
                                                           Result<IEnumerable<ServiceOrderDocumentDto>>>, IGetServiceOrderDocumentsBySOIdService
    {
        public GetServiceOrderDocumentsBySOIdService(IHttpClientFactory httpClientFactory, 
                                                     IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result<IEnumerable<ServiceOrderDocumentDto>>> Handle(int id_SO, Pagination? Pagination)
        {

            try
            {
                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/documents/all/{id_SO}";
                var result = await _httpClient.GetAsync(url);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<ServiceOrderDocumentDto>>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }
    }
}
