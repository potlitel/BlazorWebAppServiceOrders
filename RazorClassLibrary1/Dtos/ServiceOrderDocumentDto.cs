namespace RazorClassLibrary1.Dtos
{
    public class ServiceOrderDocumentDto : BaseDto
    {
        public string Name { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty ;
        public long ServiceOrderId { get; set; }
        public ServiceOrderDto ServiceOrder { get; set; } = new();
        public long DocumentTypeId { get; set; }
        public DocumentTypeDto DocumentType { get; set; } = new();
        public byte[]? File { get; set; }
        public ServiceOrderDocumentDto()
        {
            
        }

        public ServiceOrderDocumentDto(ServiceOrderDocumentDto serviceOrderDocument)
        {
            Id = serviceOrderDocument.Id;
            CreatedAt = serviceOrderDocument.CreatedAt;
            UpdatedAt = serviceOrderDocument.UpdatedAt;
            IsActive = serviceOrderDocument.IsActive;
            Name = serviceOrderDocument.Name;
            Url = serviceOrderDocument.Url;
            ServiceOrderId = serviceOrderDocument.ServiceOrderId;
            DocumentTypeId = serviceOrderDocument.DocumentTypeId;
        }
    }
}
