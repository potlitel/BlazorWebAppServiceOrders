using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Services.HttpClientSrv.ServiceOrders.GetByOwner;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrders.GetByExecutor
{
    public interface IGetServiceOrderByExecutorService
    {
        Task<Result<GetServiceOrderByUserResponse>> Handle(int id);
    }
    internal class GetServiceOrderByExecutorService : HttpClientServiceBase<GetServiceOrderByUserRequest, Result<GetServiceOrderByUserResponse>>,
                                                      IGetServiceOrderByExecutorService
    {
        public GetServiceOrderByExecutorService(IHttpClientFactory httpClientFactory, 
                                                IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result<GetServiceOrderByUserResponse>> Handle(int id)
        {
            try
            {
                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/by-executor/{id}";
                var result = await _httpClient.GetAsync(url);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return Result<GetServiceOrderByUserResponse>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }
    }
}
