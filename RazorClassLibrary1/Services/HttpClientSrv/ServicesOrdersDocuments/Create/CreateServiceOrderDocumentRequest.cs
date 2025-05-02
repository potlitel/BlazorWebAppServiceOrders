namespace RazorClassLibrary1.Services.HttpClientSrv.ServicesOrdersDocuments.Create
{
    public record CreateServiceOrderDocumentRequest(long ServiceOrderId, long DocumentTypeId, string FileName, byte[] File);
}
