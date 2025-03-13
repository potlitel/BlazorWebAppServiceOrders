using FSA.Core.ServiceOrders.Models;

namespace RazorClassLibrary1.Dtos
{
    public class SupplyDto : BaseDto
    {
        public int Amount { get; set; } = 0;
        public double Price { get; set; } = 0;
        public string Description { get; set; } = string.Empty;
        public long SupplyOperationId { get; set; }
        public SupplyOperationDto SupplyOperation { get; set; } = new();
        public long ServiceOrderTaskId { get; set; }
        public ServiceOrderTaskDocumentDto ServiceOrderTaskDocument { get; set; } = new();

        public SupplyDto()
        {
            
        }

        /// <summary>
        /// Class 's constructor: Recibe un objeto dto (SupplyDto) e inicializa la clase SupplyDto con los valores de dicho objeto
        /// </summary>
        /// <param name="dto">SupplyDto instance</param>
        public SupplyDto(SupplyDto dto)
        {
            Id = dto.Id;
            CreatedAt = dto.CreatedAt;
            UpdatedAt = dto.UpdatedAt;
            IsActive = dto.IsActive;
            Amount = dto.Amount;
            Price = dto.Price;
            Description = dto.Description;
            SupplyOperationId = dto.SupplyOperationId;
            ServiceOrderTaskId = dto.ServiceOrderTaskId;
        }

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
