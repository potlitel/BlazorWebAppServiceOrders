using FSA.Core.Dtos;
using MediatR;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTasksStates.Update
{
    public record UpdateServiceOrderTaskStateRequest(int Id, string Code, string Description, bool IsActive) : IRequest<Result>;
}
