//using FSA.Core.Dtos;
//using FSA.Core.ServiceOrders.Models;

using FSA.Core.ServiceOrders.Models;

namespace RazorClassLibrary1.Dtos
{
    public class ServiceOrderDto : BaseDto
    {
        public string Number { get; set; }
        public DateTime EstimatedEndingDate { get; set; }
        public string? Observations { get; set; }
        public string? Address { get; set; }

        public long OwnerId { get; set; }
        public long ExecutorId { get; set; }

        public long? ParentServiceOrderId { get; set; }
        public long ServiceOrderTypeId { get; set; }

        public static ServiceOrderDto ToDto(ServiceOrder entity)
        {
            if (entity is null)
                return null!;

            return new ServiceOrderDto
            {
                Id = entity.Id,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive,
                Number = entity.Number,
                EstimatedEndingDate = entity.EstimatedEndingDate,
                Observations = entity.Observations,
                Address = entity.Address,
                OwnerId = entity.OwnerId,
                ExecutorId = entity.ExecutorId,
                ParentServiceOrderId = entity.ParentServiceOrderId,
                ServiceOrderTypeId = entity.ServiceOrderTypeId,
            };
        }
    }
}
