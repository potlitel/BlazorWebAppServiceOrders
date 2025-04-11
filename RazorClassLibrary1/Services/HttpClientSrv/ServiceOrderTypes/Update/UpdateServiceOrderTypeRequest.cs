using FSA.Core.Dtos;
using MediatR;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTypes.Update
{
    public record UpdateServiceOrderTypeRequest(int Id, string Code, string Description, bool IsActive) : IRequest<Result>;
}
