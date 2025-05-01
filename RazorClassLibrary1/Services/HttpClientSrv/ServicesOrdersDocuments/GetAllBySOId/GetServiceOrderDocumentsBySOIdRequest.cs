using FSA.Core.Dtos;
using FSA.Core.Utils;
using MediatR;
using RazorClassLibrary1.Dtos;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServicesOrdersDocuments.GetAllBySOId
{
    public record GetServiceOrderDocumentsBySOIdRequest(int Id, Pagination? Pagination) : IRequest<Result<IEnumerable<ServiceOrderDocumentDto>>>;
}
