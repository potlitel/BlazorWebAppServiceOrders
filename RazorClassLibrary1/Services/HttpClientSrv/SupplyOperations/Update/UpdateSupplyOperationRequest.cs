using FSA.Core.Dtos;
using MediatR;

namespace RazorClassLibrary1.Services.HttpClientSrv.SupplyOperations.Update
{
    public record UpdateSupplyOperationRequest(int Id, string Code, string Description, bool IsActive) : IRequest<Result>;
}
