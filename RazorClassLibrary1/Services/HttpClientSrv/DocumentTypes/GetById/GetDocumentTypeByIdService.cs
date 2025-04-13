using FSA.Core.DataTypes;
using FSA.Core.Dtos;
using Microsoft.Extensions.Configuration;

namespace RazorClassLibrary1.Services.HttpClientSrv.DocumentTypes.GetById
{
    public interface IGetDocumentTypeByIdService
    {
        Task<Result<GetDocumentTypeByIdResponse>> Handle(int id);
    }
    internal class GetDocumentTypeByIdService : HttpClientServiceBase<GetDocumentTypeByIdRequest, Result<GetDocumentTypeByIdResponse>>,
                                                IGetDocumentTypeByIdService
    {
        public GetDocumentTypeByIdService(IHttpClientFactory httpClientFactory, IConfiguration configuration) : base(httpClientFactory, configuration)
        {
        }

        public async Task<Result<GetDocumentTypeByIdResponse>> Handle(int id)
        {
            try
            {
                var _httpClient = await GetWebApiAsync(HttpClientName("FSAManagement:AuthServicesSettings:Name"));

                var url = $"so/document-types/{id}";
                var result = await _httpClient.GetAsync(url);
                result.EnsureSuccessStatusCode();

                return await GetResponseAsync(result.Content);
            }
            catch (Exception ex)
            {
                return Result<GetDocumentTypeByIdResponse>.Failure([ex.Message], CustomStatusCode.StatusUnexpectedError);
            }
        }
    }
}
