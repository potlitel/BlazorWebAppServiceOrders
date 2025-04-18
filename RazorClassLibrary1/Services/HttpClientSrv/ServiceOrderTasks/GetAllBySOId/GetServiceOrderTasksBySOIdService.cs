using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using FSA.Core.Utils;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTasks.GetAllBySOId
{
    public interface IGetServiceOrderTasksBySOIdService
    {
        Task<Result<IEnumerable<ServiceOrderTaskDto>>> Handle(int id_SO, Pagination? Pagination);
    }
    internal class GetServiceOrderTasksBySOIdService : HttpClientServiceBase<GetServiceOrderTasksBySOIdRequest,
                                                       Result<IEnumerable<ServiceOrderTaskDto>>>, IGetServiceOrderTasksBySOIdService
    {
        public GetServiceOrderTasksBySOIdService(IHttpClientFactory httpClientFactory, 
                                                 IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result<IEnumerable<ServiceOrderTaskDto>>> Handle(int id_SO, Pagination? Pagination)
        {
            try
            {
                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/tasks/all/{id_SO}";
                var result = await _httpClient.GetAsync(url);
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
