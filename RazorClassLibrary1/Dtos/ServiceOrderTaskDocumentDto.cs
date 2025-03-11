using FSA.Core.ServiceOrders.Models;

namespace RazorClassLibrary1.Dtos
{
    public class ServiceOrderTaskDocumentDto : BaseDto
    {
        public required string Name { get; set; }
        public required string Url { get; set; }
        public long ServiceOrderTaskId { get; set; }
        public long DocumentTypeId { get; set; }

        public static ServiceOrderTaskDocumentDto ToDto(ServiceOrderTaskDocument entity)
        {
            if (entity is null)
                return null!;

            return new ServiceOrderTaskDocumentDto
            {
                Id = entity.Id,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive,
                Name = entity.Name,
                Url = entity.Url,
                ServiceOrderTaskId = entity.ServiceOrderTaskId,
                DocumentTypeId = entity.DocumentTypeId
            };
        }
    }
}
