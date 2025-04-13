using FSA.Core.Dtos;
using MediatR;

namespace RazorClassLibrary1.Services.HttpClientSrv.DocumentTypes.GetById
{
    public record GetDocumentTypeByIdRequest(int Id) : IRequest<Result<GetDocumentTypeByIdResponse>>;
}
