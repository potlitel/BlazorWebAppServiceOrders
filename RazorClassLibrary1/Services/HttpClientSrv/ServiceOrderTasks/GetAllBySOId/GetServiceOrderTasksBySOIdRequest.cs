using FSA.Core.Dtos;
using FSA.Core.Utils;
using MediatR;
using RazorClassLibrary1.Dtos;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTasks.GetAllBySOId
{
    public record GetServiceOrderTasksBySOIdRequest(int Id, Pagination? Pagination) : IRequest<Result<IEnumerable<ServiceOrderTaskDto>>>;
}
