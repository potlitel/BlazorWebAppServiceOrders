using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using Microsoft.Extensions.Configuration;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTypes.GetById
{
    public interface IGetServiceOrderTypeByIdService
    {
        Task<Result<GetServiceOrderTypeByIdResponse>> Handle(int id);
    }
    internal class GetServiceOrderTypeByIdService : HttpClientServiceBase<GetServiceOrderTypeByIdRequest, Result<GetServiceOrderTypeByIdResponse>>,
                                                    IGetServiceOrderTypeByIdService
    {
        public GetServiceOrderTypeByIdService(IHttpClientFactory httpClientFactory, 
                                              IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result<GetServiceOrderTypeByIdResponse>> Handle(int id)
        {
            try
            {
                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/types/{id}";
                var result = await _httpClient.GetAsync(url);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return Result<GetServiceOrderTypeByIdResponse>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }
    }
}
