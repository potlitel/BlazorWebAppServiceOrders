using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using FSA.Core.Utils;
using Microsoft.Extensions.Configuration;
using RazorClassLibrary1.Dtos;
using RazorClassLibrary1.Helpers;

namespace RazorClassLibrary1.Services.HttpClientSrv.DocumentTypes.GetAll
{
    public interface IGetAllDocumentTypesService
    {
        Task<ResultSO<DocumentTypeDto>> Handle(Pagination? pagination);
    }
    internal class GetAllDocumentTypesService : HttpClientServiceBase<GetAllDocumentTypesRequest, ResultSO<DocumentTypeDto>>,
                                                IGetAllDocumentTypesService
    {
        public GetAllDocumentTypesService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<ResultSO<DocumentTypeDto>> Handle(Pagination? pagination)
        {
            try
            {
                var request = new GetAllDocumentTypesRequest(pagination);
                HttpContent content = CreateContent(request);

                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/document-types/all";
                var result = await _httpClient.PostAsync(url, content);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                //throw ex;
                return ResultSO<DocumentTypeDto>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }

        //public async Task<GetAllDocumentTypesResponse> GetResponseAsyncc(HttpContent content)
        //{
        //    var stringContent = await content.ReadAsStringAsync();

        //    GetAllDocumentTypesResponse? response;
        //    try
        //    {
        //        //response = JsonSerializer.Deserialize<TResponse>(stringContent);
        //        response = Newtonsoft.Json.JsonConvert.DeserializeObject<GetAllDocumentTypesResponse>(stringContent);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex!;
        //    }

        //    return response!;
        //}
    }
}
