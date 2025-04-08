using FSA.Core.Utils;
using MediatR;
using RazorClassLibrary1.Dtos;
using RazorClassLibrary1.Helpers;

namespace RazorClassLibrary1.Services.HttpClientSrv.SupplyOperations.GetAll
{
    public record GetAllSupplyOperationsRequest(Pagination? Pagination) : IRequest<ResultSO<SupplyOperationDto>>;
}
