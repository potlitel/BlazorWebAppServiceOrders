using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using Microsoft.Extensions.Configuration;

namespace RazorClassLibrary1.Services.HttpClientSrv.SupplyOperations.GetById
{
    public interface IGetSupplyOperationByIdService
    {
        Task<Result<GetSupplyOperationByIdResponse>> Handle(int id);
    }
    internal class GetSupplyOperationByIdService : HttpClientServiceBase<GetSupplyOperationByIdRequest, GetSupplyOperationByIdResponse>,
                                                   IGetSupplyOperationByIdService
    {
        public GetSupplyOperationByIdService(IHttpClientFactory httpClientFactory, 
                                             IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result<GetSupplyOperationByIdResponse>> Handle(int id)
        {
            try
            {
                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/supply-operations/{id}";
                var result = await _httpClient.GetAsync(url);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return Result<GetSupplyOperationByIdResponse>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }
    }
}
