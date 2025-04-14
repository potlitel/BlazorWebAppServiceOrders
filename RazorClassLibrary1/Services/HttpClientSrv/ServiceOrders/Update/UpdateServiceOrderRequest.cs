using FSA.Core.Dtos;
using MediatR;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrders.Update
{
    public record UpdateServiceOrderRequest(int Id, string Number, DateTime EstimatedEndingDate, string? Observations, 
                                            string? Address, long OwnerId, long ExecutorId, long? ParentServiceOrderId, 
                                            long ServiceOrderTypeId) : IRequest<Result>;
}
