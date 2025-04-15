using FSA.Core.Utils;
using MediatR;
using RazorClassLibrary1.Dtos;
using RazorClassLibrary1.Helpers;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderRegisters.GetAll
{
    public record GetAllServiceOrderRegistersRequest(Pagination? Pagination) : IRequest<ResultSO<ServiceOrderRegisterDto>>;
}
