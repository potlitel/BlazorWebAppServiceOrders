using FSA.Core.Dtos;
using MediatR;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTasksStates.GetById
{
    public record GetServiceOrderTaskStateByIdRequest(int Id) : IRequest<Result<GetServiceOrderTaskStateByIdResponse>>;
}
