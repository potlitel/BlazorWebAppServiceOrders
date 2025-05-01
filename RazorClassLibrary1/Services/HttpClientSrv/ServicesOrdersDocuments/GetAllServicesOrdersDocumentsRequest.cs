using FSA.Core.Utils;
using MediatR;
using RazorClassLibrary1.Dtos;
using RazorClassLibrary1.Helpers;

namespace RazorClassLibrary1.Services.HttpClientSrv.ServicesOrdersDocuments
{
    public record GetAllServicesOrdersDocumentsRequest(Pagination pagination) : IRequest<ResultSO<ServiceOrderDocDto>>;
}
