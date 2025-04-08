using FSA.Core.Utils;
using MediatR;
using RazorClassLibrary1.Dtos;
using RazorClassLibrary1.Helpers;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTypes
{
    public record GetAllServiceOrderTypesRequest(Pagination? Pagination) : IRequest<ResultSO<ServiceOrderTypeDto>>;
}
