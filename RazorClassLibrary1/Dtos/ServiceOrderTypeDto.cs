using FSA.Core.Dtos;

namespace RazorClassLibrary1.Dtos
{
    public class ServiceOrderTypeDto : MasterDto

    {
        public ServiceOrderTypeDto()
        {
            
        }

        public ServiceOrderTypeDto(ServiceOrderTypeDto item)
        {
            Id = item.Id;
            Code = item.Code;
            Description = item.Description;
            CreatedAt = item.CreatedAt;
            UpdatedAt = item.UpdatedAt;
            IsActive = item.IsActive;
        }
    }
}
