using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using Mapster;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;

namespace RazorClassLibrary1.Services.HttpClientSrv.SupplyOperations.Update
{
    public interface IUpdateSupplyOperationService
    {
        Task<Result> Handle(SupplyOperationDto item);
    }
    internal class UpdateSupplyOperationService : HttpClientServiceBase<UpdateSupplyOperationRequest, Result>, IUpdateSupplyOperationService
    {
        public UpdateSupplyOperationService(IHttpClientFactory httpClientFactory, 
                                            IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result> Handle(SupplyOperationDto item)
        {
            try
            {
                var request = item.Adapt<UpdateSupplyOperationRequest>();
                HttpContent content = CreateContent(request);

                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/supply-operations";
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
