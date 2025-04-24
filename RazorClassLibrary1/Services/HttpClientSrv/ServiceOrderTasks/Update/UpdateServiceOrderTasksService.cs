using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using Mapster;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;
using RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTasks.Create;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTasks.Update
{
    public interface IUpdateServiceOrderTasksService
    {
        Task<Result> Handle(ServiceOrderTaskDto item);
    }
    internal class UpdateServiceOrderTasksService : HttpClientServiceBase<UpdateServiceOrderTasksRequest, Result>, IUpdateServiceOrderTasksService
    {
        public UpdateServiceOrderTasksService(IHttpClientFactory httpClientFactory, 
                                              IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result> Handle(ServiceOrderTaskDto item)
        {
            try
            {
                var request = item.Adapt<UpdateServiceOrderTasksRequest>();
                HttpContent content = CreateContent(request);

                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/tasks";
                var result = await _httpClient.PutAsync(url, content);

                //new lines
                var responseBody = await result.Content.ReadAsStringAsync();
                Console.WriteLine($"Status: {result.StatusCode}, Body: {responseBody}");

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
