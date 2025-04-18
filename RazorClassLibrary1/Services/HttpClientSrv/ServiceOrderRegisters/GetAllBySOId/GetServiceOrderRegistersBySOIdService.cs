using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using FSA.Core.Utils;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderRegisters.GetAllBySOId
{
    public interface IGetServiceOrderRegistersBySOIdService
    {
        Task<Result<IEnumerable<ServiceOrderRegisterDto>>> Handle(int id_SO, Pagination? Pagination);
    }
    internal class GetServiceOrderRegistersBySOIdService : HttpClientServiceBase<GetServiceOrderRegistersBySOIdRequest,
                                                           Result<IEnumerable<ServiceOrderRegisterDto>>>, IGetServiceOrderRegistersBySOIdService
    {
        public GetServiceOrderRegistersBySOIdService(IHttpClientFactory httpClientFactory, 
                                                     IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result<IEnumerable<ServiceOrderRegisterDto>>> Handle(int id_SO, Pagination? Pagination)
        {
            try
            {
                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/registers/all/{id_SO}";
                var result = await _httpClient.GetAsync(url);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return Result<IEnumerable<ServiceOrderRegisterDto>>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }
    }
}
