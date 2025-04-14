using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using Microsoft.Extensions.Configuration;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrders.GetByOwner
{
    public interface IGetServiceOrderByOwnerService
    {
        Task<Result<GetServiceOrderByUserResponse>> Handle(int id);
    }
    internal class GetServiceOrderByOwnerService : HttpClientServiceBase<GetServiceOrderByUserRequest, Result<GetServiceOrderByUserResponse>>,
                                                  IGetServiceOrderByOwnerService
    {
        public GetServiceOrderByOwnerService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result<GetServiceOrderByUserResponse>> Handle(int id)
        {
            try
            {
                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/by-owner/{id}";
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
