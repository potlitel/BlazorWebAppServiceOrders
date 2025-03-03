using FSA.Core.Dtos;

namespace RazorClassLibrary1.Dtos
{
    public class DocumentTypeDto : MasterDto
    {
        public DocumentTypeDto()
        {
            
        }

        public DocumentTypeDto(DocumentTypeDto item)
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
