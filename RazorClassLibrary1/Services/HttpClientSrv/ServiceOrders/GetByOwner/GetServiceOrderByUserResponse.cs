using FSA.Core.ServiceOrders.Dtos;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrders.GetByOwner
{
    public record GetServiceOrderByUserResponse(IEnumerable<ServiceOrderDto> ServiceOrderDtos);
}
