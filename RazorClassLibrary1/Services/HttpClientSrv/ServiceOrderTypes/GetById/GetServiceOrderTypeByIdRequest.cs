using FSA.Core.Dtos;
using MediatR;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServiceOrderTypes.GetById
{
    public record GetServiceOrderTypeByIdRequest(int Id) : IRequest<Result<GetServiceOrderTypeByIdResponse>>;
}
