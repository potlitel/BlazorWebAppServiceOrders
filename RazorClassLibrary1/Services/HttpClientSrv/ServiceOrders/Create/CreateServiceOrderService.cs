using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using Mapster;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;
using RazorClassLibrary1.Services.HttpClientSrv.DocumentTypes.Create;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrders.Create
{
    public interface ICreateServiceOrderService
    {
        Task<Result<CreateServiceOrderResponse>> Handle(ServiceOrderDto item);
    }
    internal class CreateServiceOrderService : HttpClientServiceBase<CreateServiceOrderRequest, Result<CreateServiceOrderResponse>>,
                                               ICreateServiceOrderService
    {
        public CreateServiceOrderService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result<CreateServiceOrderResponse>> Handle(ServiceOrderDto item)
        {
            try
            {
                var request = item.Adapt<CreateServiceOrderRequest>();
                HttpContent content = CreateContent(request);

                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so";
                var result = await _httpClient.PostAsync(url, content);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return Result<CreateServiceOrderResponse>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }
    }
}
