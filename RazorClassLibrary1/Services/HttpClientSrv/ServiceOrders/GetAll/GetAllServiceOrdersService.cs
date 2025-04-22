using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using FSA.Core.Utils;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrders.GetAll
{
    public interface IGetAllServiceOrdersService
    {
        Task<Result<IEnumerable<ServiceOrderDto>>> Handle(Pagination? pagination);
    }
    internal class GetAllServiceOrdersService : HttpClientServiceBase<GetAllServiceOrdersRequest, Result<IEnumerable<ServiceOrderDto>>>,
                                                IGetAllServiceOrdersService
    {
        public GetAllServiceOrdersService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result<IEnumerable<ServiceOrderDto>>> Handle(Pagination? pagination)
        {
            try
            {
                var request = new GetAllServiceOrdersRequest(pagination);
                HttpContent content = CreateContent(request);

                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                //var url = $"so/all";
                var url = $"so/include-related-objects/all";
                var result = await _httpClient.PostAsync(url, content);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<ServiceOrderDto>>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }
    }
}
