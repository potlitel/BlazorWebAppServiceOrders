﻿using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using Mapster;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;
using RazorClassLibrary1.Services.HttpClientSrv.Supplies.Create;
using System.Net.Http;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTasks.Create
{
    public interface ICreateServiceOrderTasksService
    {
        Task<Result<CreateServiceOrderTasksResponse>> Handle(ServiceOrderTaskDto item);
    }

    internal class CreateServiceOrderTasksService : HttpClientServiceBase<CreateServiceOrderTasksRequest, Result<CreateServiceOrderTasksResponse>>,
                                                    ICreateServiceOrderTasksService
    {
        public CreateServiceOrderTasksService(IHttpClientFactory httpClientFactory, 
                                              IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result<CreateServiceOrderTasksResponse>> Handle(ServiceOrderTaskDto item)
        {
            var jsonObject = Newtonsoft.Json.JsonConvert.SerializeObject(item);

            try
            {
                var request = item.Adapt<CreateServiceOrderTasksRequest>();
                HttpContent content = CreateContent(request);

                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/tasks";
                var result = await _httpClient.PostAsync(url, content);

                //new lines
                var responseBody = await result.Content.ReadAsStringAsync();
                Console.WriteLine($"Status: {result.StatusCode}, Body: {responseBody}");

                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return Result<CreateServiceOrderTasksResponse>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }
    }
}
