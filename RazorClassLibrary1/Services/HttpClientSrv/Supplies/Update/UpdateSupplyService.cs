using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using Mapster;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;

namespace RazorClassLibrary1.Services.HttpClientSrv.Supplies.Update
{
    public interface IUpdateSupplyService
    {
        Task<Result> Handle(SupplyDto item);
    }
    internal class UpdateSupplyService : HttpClientServiceBase<UpdateSupplyRequest, Result>, IUpdateSupplyService
    {
        public UpdateSupplyService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result> Handle(SupplyDto item)
        {
            try
            {
                var request = item.Adapt<UpdateSupplyRequest>();
                HttpContent content = CreateContent(request);

                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/supplies";
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
