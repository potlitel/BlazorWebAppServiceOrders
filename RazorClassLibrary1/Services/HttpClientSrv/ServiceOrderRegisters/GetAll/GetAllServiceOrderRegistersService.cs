using FSA.Core.DataTypes;
using FSA.Core.Utils;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;
using RazorClassLibrary1.Helpers;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderRegisters.GetAll
{
    public interface IGetAllServiceOrderRegistersService
    {
        Task<ResultSO<ServiceOrderRegisterDto>> Handle(Pagination? pagination);
    }
    internal class GetAllServiceOrderRegistersService : HttpClientServiceBase<GetAllServiceOrderRegistersRequest, ResultSO<ServiceOrderRegisterDto>>,
                                                        IGetAllServiceOrderRegistersService
    {
        public GetAllServiceOrderRegistersService(IHttpClientFactory httpClientFactory, 
                                                  IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<ResultSO<ServiceOrderRegisterDto>> Handle(Pagination? pagination)
        {
            try
            {
                var request = new GetAllServiceOrderRegistersRequest(pagination);
                HttpContent content = CreateContent(request);

                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/registers/all";
                var result = await _httpClient.PostAsync(url, content);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return ResultSO<ServiceOrderRegisterDto>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }
    }
}
