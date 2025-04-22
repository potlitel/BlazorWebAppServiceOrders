using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using FSA.Core.Utils;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;

namespace RazorClassLibrary1.Services.HttpClientSrv.Supplies.GetAll
{
    public interface IGetAllSuppliesService
    {
        Task<Result<IEnumerable<SupplyDto>>> Handle(Pagination? pagination);
    }
    internal class GetAllSuppliesService : HttpClientServiceBase<GetAllSuppliesRequest, Result<IEnumerable<SupplyDto>>>, IGetAllSuppliesService
    {
        public GetAllSuppliesService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result<IEnumerable<SupplyDto>>> Handle(Pagination? pagination)
        {
            try
            {
                var request = new GetAllSuppliesRequest(pagination);
                HttpContent content = CreateContent(request);

                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                //var url = $"so/supplies/all";
                var url = $"so/supplies/include-related-objects/all";
                var result = await _httpClient.PostAsync(url, content);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<SupplyDto>>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }
    }
}
