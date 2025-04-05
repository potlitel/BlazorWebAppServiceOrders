using FSA.Core.Dtos;
using FSA.Core.Utils;
using MediatR;

namespace RazorClassLibrary1.Services.HttpClientSrv.DocumentTypes.GetAll
{
    //public record GetAllDocumentTypesRequest(Pagination? Pagination) : IRequest<Result<GetAllDocumentTypesResponse>>;
    public record GetAllDocumentTypesRequest(Pagination? Pagination) : IRequest<Result<GetAllDocumentTypesResponse>>;
}
