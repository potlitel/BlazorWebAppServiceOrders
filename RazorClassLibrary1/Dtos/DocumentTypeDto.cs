using FSA.Core.Dtos;
using FSA.Core.Utils;

namespace RazorClassLibrary1.Dtos
{
    public class DocumentTypeDto : MasterDto
    {
        //public int Id { get; set; }
        //public string Code { get; set; } = string.Empty;
        //public string Description { get; set; } = string.Empty;
        //public DateTime CreatedAt { get; set; } = DateTimeHelper.Now();
        //public DateTime UpdatedAt { get; set; } = DateTimeHelper.Now();
        //public bool IsActive { get; set; } = true;

        public DocumentTypeDto()
        {
            
        }

        public DocumentTypeDto(DocumentTypeDto item)
        {
            Id = Convert.ToInt32(item.Id);
            Code = item.Code;
            Description = item.Description;
            CreatedAt = item.CreatedAt;
            UpdatedAt = item.UpdatedAt;
            IsActive = item.IsActive;
        }
    }
}
