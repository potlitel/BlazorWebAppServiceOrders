//using FSA.Core.Dtos;
//using FSA.Core.ServiceOrders.Models;

using FSA.Core.ServiceOrders.Models;
using FSA.Core.ServiceOrders.Models.Masters;
using FSA.Management.Application.Features.Users;

namespace RazorClassLibrary1.Dtos
{
    public class ServiceOrderDto : BaseDto
    {
        public string Number { get; set; }
        public DateTime EstimatedEndingDate { get; set; }
        public string? Observations { get; set; }
        public string? Address { get; set; }
        public long OwnerId { get; set; }
        public UserDto Owner { get; set; }
        public long ExecutorId { get; set; }
        public UserDto Executor { get; set; }
        public long? ParentServiceOrderId { get; set; }
        public virtual ServiceOrderDto? ParentServiceOrder { get; set; }
        public long ServiceOrderTypeId { get; set; }
        public virtual ServiceOrderTypeDto? Type { get; set; }

        public virtual ICollection<ServiceOrderDocumentDto> Documents { get; set; } = [];
        public virtual ICollection<ServiceOrderTaskDto> Tasks { get; set; } = [];
        public virtual ICollection<ServiceOrderRegisterDto> Registers { get; set; } = [];
        public virtual ICollection<ServiceOrderFeatureDto> Features { get; set; } = [];

        public ServiceOrderDto()
        {
            
        }

        /// <summary>
        /// Class 's constructor: Recibe un objeto dto (ServiceOrderDto) e inicializa la clase ServiceOrderDto con los valores de dicho objeto
        /// </summary>
        /// <param name="dto">ServiceOrderDto instance</param>
        public ServiceOrderDto(ServiceOrderDto dto)
        {
            Id = dto.Id;
            CreatedAt = dto.CreatedAt;
            UpdatedAt = dto.UpdatedAt;
            IsActive = dto.IsActive;
            Number = dto.Number;
            EstimatedEndingDate = dto.EstimatedEndingDate;
            Observations = dto.Observations;
            Address = dto.Address;
            OwnerId = dto.OwnerId;
            ExecutorId = dto.ExecutorId;
            ParentServiceOrder = dto.ParentServiceOrder;
            ParentServiceOrderId = dto.ParentServiceOrderId;
            Type = dto.Type;
            ServiceOrderTypeId = dto.ServiceOrderTypeId;
            Documents = dto.Documents;
            Tasks = dto.Tasks;
            Registers = dto.Registers;
            Features = dto.Features;
        }

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
