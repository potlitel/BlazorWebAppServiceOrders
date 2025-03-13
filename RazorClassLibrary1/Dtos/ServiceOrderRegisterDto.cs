using FSA.Core.ServiceOrders.Models;

namespace RazorClassLibrary1.Dtos
{
    public class ServiceOrderRegisterDto : BaseDto
    {
        public string Trigger { get; set; } = string.Empty;
        public string StateFrom { get; set; } = string.Empty;
        public string StateTo { get; set; } = string.Empty;
        public string? Observations { get; set; }
        public long ServiceOrderId { get; set; }
        public ServiceOrderDto ServiceOrder { get; set; } = new();

        public ServiceOrderRegisterDto()
        {
            
        }

        /// <summary>
        /// Class 's constructor: Recibe un objeto dto (ServiceOrderRegisterDto) e inicializa la clase ServiceOrderRegisterDto con los valores de dicho objeto
        /// </summary>
        /// <param name="dto">ServiceOrderRegisterDto instance</param>
        public ServiceOrderRegisterDto(ServiceOrderRegisterDto dto)
        {
            Id = dto.Id;
            CreatedAt = dto.CreatedAt;
            UpdatedAt = dto.UpdatedAt;
            IsActive = dto.IsActive;
            Trigger = dto.Trigger;
            StateFrom = dto.StateFrom;
            StateTo = dto.StateTo;
            Observations = dto.Observations;
            ServiceOrderId = dto.ServiceOrderId;
        }

        public static ServiceOrderRegisterDto ToDto(ServiceOrderRegister entity)
        {
            if (entity is null)
                return null!;

            return new ServiceOrderRegisterDto
            {
                Id = entity.Id,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive,
                Trigger = entity.Trigger,
                StateFrom = entity.StateFrom,
                StateTo = entity.StateTo,
                Observations = entity.Observations,
                ServiceOrderId = entity.ServiceOrderId
            };
        }
    }
}
