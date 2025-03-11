using FSA.Core.ServiceOrders.Models;

namespace RazorClassLibrary1.Dtos
{
    public class SupplyDto : BaseDto
    {
        public required int Amount { get; set; }
        public required double Price { get; set; }
        public required string Description { get; set; } = string.Empty;
        public long SupplyOperationId { get; set; }
        public long ServiceOrderTaskId { get; set; }

        public static SupplyDto ToDto(Supply entity)
        {
            if (entity is null)
                return null!;

            return new SupplyDto
            {
                Id = entity.Id,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive,
                Amount = entity.Amount,
                Price = entity.Price,
                Description = entity.Description,
                SupplyOperationId = entity.SupplyOperationId,
                ServiceOrderTaskId = entity.ServiceOrderTaskId
            };
        }
    }
}
