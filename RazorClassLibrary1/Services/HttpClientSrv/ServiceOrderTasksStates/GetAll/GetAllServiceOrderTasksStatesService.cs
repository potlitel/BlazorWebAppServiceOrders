using FSA.Core.DataTypes;
using FSA.Core.Utils;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;
using RazorClassLibrary1.Helpers;
using RazorClassLibrary1.Services.HttpClientSrv.DocumentTypes.GetAll;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTasksStates.GetAll
{
    public interface IGetAllServiceOrderTasksStatesService
    {
        Task<ResultSO<ServiceOrderTaskStateDto>> Handle(Pagination? pagination);
    }


    internal class GetAllServiceOrderTasksStatesService : HttpClientServiceBase<GetAllServiceOrderTasksStatesRequest, ResultSO<ServiceOrderTaskStateDto>>,
                                                          IGetAllServiceOrderTasksStatesService
    {
        public GetAllServiceOrderTasksStatesService(IHttpClientFactory httpClientFactory, IConfiguration configuration) 
                                                   : base(httpClientFactory, configuration)
        {
        }

        public async Task<ResultSO<ServiceOrderTaskStateDto>> Handle(Pagination? pagination)
        {
            try
            {
                var request = new GetAllServiceOrderTasksStatesRequest(pagination);
                HttpContent content = CreateContent(request);

                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/task-states/all";
                var result = await _httpClient.PostAsync(url, content);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return ResultSO<ServiceOrderTaskStateDto>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }
    }
}
