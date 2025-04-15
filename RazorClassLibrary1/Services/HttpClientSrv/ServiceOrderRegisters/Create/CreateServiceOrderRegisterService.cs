using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using Mapster;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderRegisters.Create
{
    public interface ICreateServiceOrderRegisterService
    {
        Task<Result<CreateServiceOrderRegisterResponse>> Handle(ServiceOrderRegisterDto item);
    }

    internal class CreateServiceOrderRegisterService : HttpClientServiceBase<CreateServiceOrderRegisterRequest, Result<CreateServiceOrderRegisterResponse>>,
                                                       ICreateServiceOrderRegisterService
    {
        public CreateServiceOrderRegisterService(IHttpClientFactory httpClientFactory, 
                                                 IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result<CreateServiceOrderRegisterResponse>> Handle(ServiceOrderRegisterDto item)
        {
            try
            {
                var request = item.Adapt<CreateServiceOrderRegisterRequest>();
                HttpContent content = CreateContent(request);

                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/registers";
                var result = await _httpClient.PostAsync(url, content);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return Result<CreateServiceOrderRegisterResponse>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }
    }
}
