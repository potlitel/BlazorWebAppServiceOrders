using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Services.HttpClientSrv.DocumentTypes.GetById;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTasksStates.GetById
{
    public interface IGetServiceOrderTaskStateByIdService
    {
        Task<Result<GetServiceOrderTaskStateByIdResponse>> Handle(int id);
    }
    internal class GetServiceOrderTaskStateByIdService : HttpClientServiceBase<GetServiceOrderTaskStateByIdRequest,
                                                                               Result<GetServiceOrderTaskStateByIdResponse>>,
                                                         IGetServiceOrderTaskStateByIdService
    {
        public GetServiceOrderTaskStateByIdService(IHttpClientFactory httpClientFactory, 
                                                   IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result<GetServiceOrderTaskStateByIdResponse>> Handle(int id)
        {
            try
            {
                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/task-states/{id}";
                var result = await _httpClient.GetAsync(url);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return Result<GetServiceOrderTaskStateByIdResponse>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }
    }
}
