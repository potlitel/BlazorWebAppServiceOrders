using FSA.Core.Utils;
using MediatR;
using RazorClassLibrary1.Dtos;
using RazorClassLibrary1.Helpers;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrders.GetAll
{
    public record GetAllServiceOrdersRequest(Pagination? Pagination) : IRequest<ResultSO<ServiceOrderDto>>;
}
