using FSA.Core.Dtos;
using MediatR;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrders.GetByOwner
{
    public record GetServiceOrderByUserRequest(int Id) : IRequest<Result<GetServiceOrderByUserResponse>>;
}
