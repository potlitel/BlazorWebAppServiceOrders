using FSA.Core.DataTypes;
using FSA.Core.Utils;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;
using RazorClassLibrary1.Helpers;

namespace RazorClassLibrary1.Services.HttpClientSrv.SupplyOperations.GetAll
{
    public interface IGetAllSupplyOperationsService
    {
        Task<ResultSO<SupplyOperationDto>> Handle(Pagination? pagination);
    }
    internal class GetAllSupplyOperationsService : HttpClientServiceBase<GetAllSupplyOperationsRequest, ResultSO<SupplyOperationDto>>,
                                                   IGetAllSupplyOperationsService
    {
        public GetAllSupplyOperationsService(IHttpClientFactory httpClientFactory, 
                                             IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<ResultSO<SupplyOperationDto>> Handle(Pagination? pagination)
        {
            try
            {
                var request = new GetAllSupplyOperationsRequest(pagination);
                HttpContent content = CreateContent(request);

                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/supply-operations/all";
                var result = await _httpClient.PostAsync(url, content);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return ResultSO<SupplyOperationDto>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }
    }
}
