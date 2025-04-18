using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using FSA.Core.Utils;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTasks.GetAll
{
    public interface IGetAllServiceOrdersTasksService
    {
        Task<Result<IEnumerable<ServiceOrderTaskDto>>> Handle(Pagination? pagination);
    }
    internal class GetAllServiceOrdersTasksService : HttpClientServiceBase<GetAllServiceOrdersTasksRequest, Result<IEnumerable<ServiceOrderTaskDto>>>,
                                                     IGetAllServiceOrdersTasksService
    {
        public GetAllServiceOrdersTasksService(IHttpClientFactory httpClientFactory, 
                                               IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result<IEnumerable<ServiceOrderTaskDto>>> Handle(Pagination? pagination)
        {
            try
            {
                var request = new GetAllServiceOrdersTasksRequest(pagination);
                HttpContent content = CreateContent(request);

                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/tasks/all";
                var result = await _httpClient.PostAsync(url, content);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<ServiceOrderTaskDto>>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }
    }
}
