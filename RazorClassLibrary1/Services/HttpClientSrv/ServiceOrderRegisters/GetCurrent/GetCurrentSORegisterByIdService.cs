using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Helpers;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderRegisters.GetCurrent
{
    public interface IGetCurrentSORegisterByIdService
    {
        Task<Root> Handle(int id);
    }
    internal class GetCurrentSORegisterByIdService : HttpClientServiceBase<GetCurrentSORegisterByIdRequest, Root>,
                                                     IGetCurrentSORegisterByIdService
    {
        public GetCurrentSORegisterByIdService(IHttpClientFactory httpClientFactory, 
                                               IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Root> Handle(int id)
        {
            try
            {
                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                //var url = $"so/registers/current/{id}";
                var url = "so/registers/current/{id_so}?id="+id;
                var result = await _httpClient.GetAsync(url);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return new Root();
            }
        }
    }
}
