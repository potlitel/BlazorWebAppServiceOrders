using FSA.Core.Dtos;
using MediatR;

namespace RazorClassLibrary1.Services.HttpClientSrv.DocumentTypes.Update
{
    public record UpdateDocumentTypeRequest(int Id, string Code, string Description, bool IsActive) : IRequest<Result>;
}
