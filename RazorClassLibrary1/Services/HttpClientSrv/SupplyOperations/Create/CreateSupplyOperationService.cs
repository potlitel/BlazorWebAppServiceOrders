using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using Mapster;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;

namespace RazorClassLibrary1.Services.HttpClientSrv.SupplyOperations.Create
{
    public interface ICreateSupplyOperationService
    {
        Task<Result<CreateSupplyOperationResponse>> Handle(SupplyOperationDto item);
    }
    internal class CreateSupplyOperationService : HttpClientServiceBase<CreateSupplyOperationRequest, Result<CreateSupplyOperationResponse>>,
                                                  ICreateSupplyOperationService
    {
        public CreateSupplyOperationService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result<CreateSupplyOperationResponse>> Handle(SupplyOperationDto item)
        {
            try
            {
                var request = item.Adapt<CreateSupplyOperationRequest>();
                HttpContent content = CreateContent(request);

                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/supply-operations";
                var result = await _httpClient.PostAsync(url, content);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return Result<CreateSupplyOperationResponse>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }
    }
}
