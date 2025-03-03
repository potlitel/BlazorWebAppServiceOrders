using FSA.Core.Dtos;

namespace RazorClassLibrary1.Dtos
{
    public class SupplyOperationDto : MasterDto
    {
        public SupplyOperationDto()
        {
            
        }

        public SupplyOperationDto(SupplyOperationDto item)
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
