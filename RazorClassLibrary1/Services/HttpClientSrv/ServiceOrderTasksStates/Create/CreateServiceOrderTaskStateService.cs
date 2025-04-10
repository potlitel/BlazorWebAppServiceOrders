using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using Mapster;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTasksStates.Create
{
    public interface ICreateServiceOrderTaskStateService
    {
        Task<Result<CreateServiceOrderTaskStateResponse>> Handle(ServiceOrderTaskStateDto item);
    }
    internal class CreateServiceOrderTaskStateService : HttpClientServiceBase<CreateServiceOrderTaskStateRequest, Result<CreateServiceOrderTaskStateResponse>>,
                                                   ICreateServiceOrderTaskStateService
    {
        public CreateServiceOrderTaskStateService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result<CreateServiceOrderTaskStateResponse>> Handle(ServiceOrderTaskStateDto item)
        {
            try
            {
                var request = item.Adapt<CreateServiceOrderTaskStateRequest>();
                HttpContent content = CreateContent(request);

                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/task-states";
                var result = await _httpClient.PostAsync(url, content);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return Result<CreateServiceOrderTaskStateResponse>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }
    }
}
