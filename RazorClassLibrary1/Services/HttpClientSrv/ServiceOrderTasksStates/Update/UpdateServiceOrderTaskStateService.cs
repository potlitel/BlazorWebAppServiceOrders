using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using Mapster;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTasksStates.Update
{
    public interface IUpdateServiceOrderTaskStateService
    {
        Task<Result> Handle(ServiceOrderTaskStateDto item);
    }
    internal class UpdateServiceOrderTaskStateService : HttpClientServiceBase<UpdateServiceOrderTaskStateRequest, Result>,
                                                        IUpdateServiceOrderTaskStateService
    {
        public UpdateServiceOrderTaskStateService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : 
                                                  base(httpClientFactory, configuration)
        {
        }

        public async Task<Result> Handle(ServiceOrderTaskStateDto item)
        {
            try
            {
                var request = item.Adapt<UpdateServiceOrderTaskStateRequest>();
                HttpContent content = CreateContent(request);

                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/task-states";
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
