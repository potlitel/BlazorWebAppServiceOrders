using FSA.Core.BaseEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary1.Dtos
{
    public class MasterDto : BaseDto
    {
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;

        public static MasterDto ToDto<T>(T entity) where T : Master
        {
            if (entity is null)
                return null!;

            return new MasterDto
            {
                Id = entity.Id,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive,
                Code = entity.Code,
                Description = entity.Description
            };
        }
    }
}
