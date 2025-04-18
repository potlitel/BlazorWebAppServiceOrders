using FSA.Core.Dtos;
using FSA.Core.Utils;
using MediatR;
using RazorClassLibrary1.Dtos;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderRegisters.GetAllBySOId
{
    public record GetServiceOrderRegistersBySOIdRequest(int Id, Pagination? Pagination) : IRequest<Result<IEnumerable<ServiceOrderRegisterDto>>>;
}
