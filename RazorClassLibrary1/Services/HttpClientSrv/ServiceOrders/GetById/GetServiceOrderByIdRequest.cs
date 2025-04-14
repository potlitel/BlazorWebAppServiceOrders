using FSA.Core.Dtos;
using MediatR;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrders.GetById
{
    public record GetServiceOrderByIdRequest(int Id) : IRequest<Result<GetServiceOrderByIdResponse>>;
}
