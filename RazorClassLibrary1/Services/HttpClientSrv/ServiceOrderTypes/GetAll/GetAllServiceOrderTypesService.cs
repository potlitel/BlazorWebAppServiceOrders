using FSA.Core.DataTypes;
using FSA.Core.Utils;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;
using RazorClassLibrary1.Helpers;
using RazorClassLibrary1.Services.HttpClientSrv.DocumentTypes.GetAll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTypes.GetAll
{
    public interface IGetAllServiceOrderTypesService
    {
        Task<ResultSO<ServiceOrderTypeDto>> Handle(Pagination? pagination);
    }

    internal class GetAllServiceOrderTypesService : HttpClientServiceBase<GetAllServiceOrderTypesRequest, ResultSO<ServiceOrderTypeDto>>,
                                                    IGetAllServiceOrderTypesService
    {
        public GetAllServiceOrderTypesService(IHttpClientFactory httpClientFactory,
                                              IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<ResultSO<ServiceOrderTypeDto>> Handle(Pagination? pagination)
        {
            try
            {
                var request = new GetAllServiceOrderTypesRequest(pagination);
                HttpContent content = CreateContent(request);

                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/types/all";
                var result = await _httpClient.PostAsync(url, content);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return ResultSO<ServiceOrderTypeDto>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }
    }
}
