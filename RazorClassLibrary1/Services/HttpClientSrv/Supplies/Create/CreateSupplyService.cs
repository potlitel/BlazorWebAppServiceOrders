using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using Mapster;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;

namespace RazorClassLibrary1.Services.HttpClientSrv.Supplies.Create
{
    public interface ICreateSupplyService
    {
        Task<Result<CreateSupplyResponse>> Handle(SupplyDto item);
    }
    internal class CreateSupplyService : HttpClientServiceBase<CreateSupplyRequest, Result<CreateSupplyResponse>>, ICreateSupplyService
    {
        public CreateSupplyService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result<CreateSupplyResponse>> Handle(SupplyDto item)
        {
            try
            {
                var request = item.Adapt<CreateSupplyRequest>();
                HttpContent content = CreateContent(request);

                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/supplies";
                var result = await _httpClient.PostAsync(url, content);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return Result<CreateSupplyResponse>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }
    }
}
