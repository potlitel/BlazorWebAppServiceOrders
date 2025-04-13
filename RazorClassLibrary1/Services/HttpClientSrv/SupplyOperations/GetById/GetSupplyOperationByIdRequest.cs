using FSA.Core.Dtos;
using MediatR;

namespace RazorClassLibrary1.Services.HttpClientSrv.SupplyOperations.GetById
{
    public record GetSupplyOperationByIdRequest(int Id) : IRequest<Result<GetSupplyOperationByIdResponse>>;
}
