using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Services.HttpClientSrv.DocumentTypes.GetById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrders.GetById
{
    public interface IGetServiceOrderByIdService
    {
        Task<Result<GetServiceOrderByIdResponse>> Handle(int id);
    }
    internal class GetServiceOrderByIdService : HttpClientServiceBase<GetServiceOrderByIdRequest, Result<GetServiceOrderByIdResponse>>,
                                                IGetServiceOrderByIdService
    {
        public GetServiceOrderByIdService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result<GetServiceOrderByIdResponse>> Handle(int id)
        {
            try
            {
                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/{id}";
                var result = await _httpClient.GetAsync(url);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return Result<GetServiceOrderByIdResponse>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }
    }
}
