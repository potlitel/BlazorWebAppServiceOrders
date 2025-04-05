using FSA.Core.Utils;
using MediatR;
using RazorClassLibrary1.Dtos;
using RazorClassLibrary1.Helpers;

namespace RazorClassLibrary1.Services.HttpClientSrv.DocumentTypes.GetAll
{
    public record GetAllDocumentTypesRequest(Pagination? Pagination) : IRequest<ResultSO<DocumentTypeDto>>;
}
