using FSA.Core.Dtos;

namespace RazorClassLibrary1.Dtos
{
    public class ServiceOrderTaskStateDto : MasterDto
    {
        public ServiceOrderTaskStateDto()
        {
            
        }

        public ServiceOrderTaskStateDto(ServiceOrderTaskStateDto item)
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
