using FSA.Core.DataTypes;
using FSA.Core.Utils;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;
using RazorClassLibrary1.Helpers;
using RazorClassLibrary1.Services.HttpClientSrv.DocumentTypes.GetAll;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrders.GetAll
{
    public interface IGetAllServiceOrdersService
    {
        Task<ResultSO<ServiceOrderDto>> Handle(Pagination? pagination);
    }
    internal class GetAllServiceOrdersService : HttpClientServiceBase<GetAllServiceOrdersRequest, ResultSO<ServiceOrderDto>>,
                                                IGetAllServiceOrdersService
    {
        public GetAllServiceOrdersService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<ResultSO<ServiceOrderDto>> Handle(Pagination? pagination)
        {
            try
            {
                var request = new GetAllServiceOrdersRequest(pagination);
                HttpContent content = CreateContent(request);

                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/all";
                var result = await _httpClient.PostAsync(url, content);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return ResultSO<ServiceOrderDto>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }
    }
}
