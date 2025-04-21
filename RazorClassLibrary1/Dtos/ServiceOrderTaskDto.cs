using FSA.Core.ServiceOrders.Models;

namespace RazorClassLibrary1.Dtos
{
    public class ServiceOrderTaskDto : BaseDto
    {
        public string? Observations { get; set; }
        public DateTime ExecutionDate { get; set; }

        public long ServiceOrderTaskStateId { get; set; }
        public virtual ServiceOrderTaskStateDto? ServiceOrderTaskState { get; set; }
        public long ServiceOrderId { get; set; }
        public virtual ServiceOrderDto? ServiceOrder { get; set; }
        public virtual ICollection<SupplyDto> Supplies { get; set; } = [];
        public virtual ICollection<ServiceOrderTaskDocumentDto> Documents { get; set; } = [];

        public ServiceOrderTaskDto()
        {
            
        }

        public ServiceOrderTaskDto(ServiceOrderTaskDto dto)
        {
            Observations = dto.Observations;
            ExecutionDate = dto.ExecutionDate;
            ServiceOrderTaskState = dto.ServiceOrderTaskState;
            ServiceOrderTaskStateId = dto.ServiceOrderTaskStateId;
            ServiceOrder = dto.ServiceOrder;
            ServiceOrderId = dto.ServiceOrderId;
            Supplies = dto.Supplies;
            Documents = dto.Documents;
        }

        public static ServiceOrderTaskDto ToDto(ServiceOrderTask entity)
        {
            if (entity is null)
                return null!;

            return new ServiceOrderTaskDto
            {
                Id = entity.Id,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive,
                Observations = entity.Observations,
                ExecutionDate = entity.ExecutionDate,
                ServiceOrderTaskStateId = entity.ServiceOrderTaskStateId,
                ServiceOrderId = entity.ServiceOrderId,
                Supplies = (ICollection<SupplyDto>)entity.Supplies,
                Documents = (ICollection<ServiceOrderTaskDocumentDto>)entity.Documents
            };
        }
    }
}
