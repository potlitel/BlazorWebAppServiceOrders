using FSA.Core.Dtos;
using MediatR;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderRegisters.GetCurrent
{
    public record GetCurrentSORegisterByIdRequest(int Id) : IRequest<Result<GetCurrentSORegisterByIdResponse>>;
}
