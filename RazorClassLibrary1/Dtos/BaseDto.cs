using FSA.Core.Utils;

namespace RazorClassLibrary1.Dtos
{
    public class BaseDto
    {
        public long Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTimeHelper.Now();
        public DateTime UpdatedAt { get; set; } = DateTimeHelper.Now();
        public bool IsActive { get; set; } = true;
    }
}
