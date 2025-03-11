//using FSA.Core.Dtos;
//using FSA.Core.ServiceOrders.Models;

using FSA.Core.ServiceOrders.Models;

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

        public static ServiceOrderDocumentDto ToDto(ServiceOrderDocument entity)
        {
            if (entity is null)
                return null!;

            return new ServiceOrderDocumentDto
            {
                Id = entity.Id,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive,
                Name = entity.Name,
                Url = entity.Url,
                ServiceOrderId = entity.ServiceOrderId,
                DocumentTypeId = entity.DocumentTypeId
            };
        }
    }
}
