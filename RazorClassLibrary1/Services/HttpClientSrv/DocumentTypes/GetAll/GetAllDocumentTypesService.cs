using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using FSA.Core.Utils;
using Microsoft.Extensions.Configuration;

namespace RazorClassLibrary1.Services.HttpClientSrv.DocumentTypes.GetAll
{
    public interface IGetAllDocumentTypesService
    {
        //Task<Result<GetAllDocumentTypesResponse>> Handle(Pagination? pagination);
        Task<Root> Handle(Pagination? pagination);
    }
    internal class GetAllDocumentTypesService : HttpClientServiceBase<GetAllDocumentTypesRequest, Result<GetAllDocumentTypesResponse>>,
                                                IGetAllDocumentTypesService
    {
        public GetAllDocumentTypesService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Root> Handle(Pagination? pagination)
        {
            try
            {
                var request = new GetAllDocumentTypesRequest(pagination);
                HttpContent content = CreateContent(request);

                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/document-types/all";
                var result = await _httpClient.PostAsync(url, content);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsyncc(result.Content);
            }
            catch (Exception ex)
            {
                throw ex;
                //return Result<GetAllDocumentTypesResponse>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }

        public async Task<Root> GetResponseAsyncc(HttpContent content)
        {
            var stringContent = await content.ReadAsStringAsync();

            Root? response;
            try
            {
                //response = JsonSerializer.Deserialize<TResponse>(stringContent);
                response = Newtonsoft.Json.JsonConvert.DeserializeObject<Root>(stringContent);
            }
            catch (Exception ex)
            {
                throw ex!;
            }

            return response!;
        }
    }
}
