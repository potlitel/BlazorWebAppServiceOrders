using FSA.Core.ServiceOrders.Models;

namespace RazorClassLibrary1.Dtos
{
    public class ServiceOrderFeatureDto : BaseDto
    {
        public required string FeatureId { get; set; } = string.Empty;
        public required string Wkt { get; set; } = string.Empty;
        public required long ServiceOrderId { get; set; }

        public static ServiceOrderFeatureDto ToDto(ServiceOrderFeature entity)
        {
            if (entity is null)
                return null!;

            return new ServiceOrderFeatureDto
            {
                Id = entity.Id,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive,
                FeatureId = entity.FeatureId,
                Wkt = entity.Wkt,
                ServiceOrderId = entity.ServiceOrderId
            };
        }
    }
}
