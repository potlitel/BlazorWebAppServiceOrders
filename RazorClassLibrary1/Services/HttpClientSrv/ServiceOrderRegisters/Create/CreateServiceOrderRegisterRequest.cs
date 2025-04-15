using FSA.Core.Dtos;
using MediatR;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderRegisters.Create
{
    public record CreateServiceOrderRegisterRequest(string trigger, string stateFrom, string stateTo,
                                                    string observations, int serviceOrderId) : IRequest<Result<CreateServiceOrderRegisterResponse>>;
}
