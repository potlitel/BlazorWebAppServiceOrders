using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using Mapster;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrders.Update
{
    public interface IUpdateServiceOrderService
    {
        Task<Result> Handle(ServiceOrderDto item);
    }
    internal class UpdateServiceOrderService : HttpClientServiceBase<UpdateServiceOrderRequest, Result>, IUpdateServiceOrderService
    {
        public UpdateServiceOrderService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result> Handle(ServiceOrderDto item)
        {
            try
            {
                var request = item.Adapt<UpdateServiceOrderRequest>();
                HttpContent content = CreateContent(request);

                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/????";
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
