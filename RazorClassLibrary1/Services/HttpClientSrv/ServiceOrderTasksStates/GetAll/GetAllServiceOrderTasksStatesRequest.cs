using FSA.Core.Utils;
using MediatR;
using RazorClassLibrary1.Dtos;
using RazorClassLibrary1.Helpers;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTasksStates.GetAll
{
    public record GetAllServiceOrderTasksStatesRequest(Pagination? Pagination) : IRequest<ResultSO<ServiceOrderTaskStateDto>>;
}
