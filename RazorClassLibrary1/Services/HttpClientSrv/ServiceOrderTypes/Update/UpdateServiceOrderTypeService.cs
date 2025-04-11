using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using Mapster;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTypes.Update
{
    public interface IUpdateServiceOrderTypeService
    {
        Task<Result> Handle(ServiceOrderTypeDto item);
    }
    internal class UpdateServiceOrderTypeService : HttpClientServiceBase<UpdateServiceOrderTypeRequest, Result>, IUpdateServiceOrderTypeService
    {
        public UpdateServiceOrderTypeService(IHttpClientFactory httpClientFactory, 
                                             IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result> Handle(ServiceOrderTypeDto item)
        {
            try
            {
                var request = item.Adapt<UpdateServiceOrderTypeRequest>();
                HttpContent content = CreateContent(request);

                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/types";
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
