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
