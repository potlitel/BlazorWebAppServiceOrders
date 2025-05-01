using RazorClassLibrary1.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RazorClassLibrary1.Dtos
{
    public class ServiceOrderDocDto : BaseDto
    {
        public required string Name { get; set; }
        public required string Url { get; set; }
        public long ServiceOrderId { get; set; }
        public virtual ServiceOrderDto? ServiceOrder { get; set; }
        public long DocumentTypeId { get; set; }
        public virtual DocumentTypeDto? DocumentType { get; set; }

        public ServiceOrderDocDto()
        {
            
        }

        public ServiceOrderDocDto(ServiceOrderDocDto orderDocDto)
        {
            Id = orderDocDto.Id;
            CreatedAt = orderDocDto.CreatedAt;
            UpdatedAt = orderDocDto.UpdatedAt;
            IsActive = orderDocDto.IsActive;
            Name = orderDocDto.Name;
            Url = orderDocDto.Url;
            ServiceOrder = orderDocDto.ServiceOrder;
            ServiceOrderId = orderDocDto.ServiceOrderId;
            DocumentType = orderDocDto.DocumentType;
            DocumentTypeId = orderDocDto.DocumentTypeId;
        }
    }
}
