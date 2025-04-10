using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using Mapster;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTypes.Create
{
    public interface ICreateServiceOrderTypeService
    {
        Task<Result<CreateServiceOrderTypeResponse>> Handle(ServiceOrderTypeDto item);
    }
    internal class CreateServiceOrderTypeService : HttpClientServiceBase<CreateServiceOrderTypeRequest, Result<CreateServiceOrderTypeResponse>>,
                                                   ICreateServiceOrderTypeService
    {
        public CreateServiceOrderTypeService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result<CreateServiceOrderTypeResponse>> Handle(ServiceOrderTypeDto item)
        {
            try
            {
                var request = item.Adapt<CreateServiceOrderTypeRequest>();
                HttpContent content = CreateContent(request);

                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/types";
                var result = await _httpClient.PostAsync(url, content);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return Result<CreateServiceOrderTypeResponse>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }
    }
}
