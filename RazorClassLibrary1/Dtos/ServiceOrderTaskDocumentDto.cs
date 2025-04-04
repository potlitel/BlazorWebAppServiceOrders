﻿using FSA.Core.ServiceOrders.Models;

namespace RazorClassLibrary1.Dtos
{
    public class ServiceOrderTaskDocumentDto : BaseDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public long ServiceOrderTaskId { get; set; }
        //public ServiceOrderTaskDto ServiceOrderTask { get; set; } = new();
        public long DocumentTypeId { get; set; }
        public DocumentTypeDto DocumentType { get; set; } = new();

        public ServiceOrderTaskDocumentDto()
        {
            
        }

        /// <summary>
        /// Class 's constructor: Recibe un objeto dto (ServiceOrderTaskDocumentDto) e inicializa la clase ServiceOrderTaskDocumentDto con los valores de dicho objeto
        /// </summary>
        /// <param name="dto">ServiceOrderTaskDocumentDto instance</param>
        public ServiceOrderTaskDocumentDto(ServiceOrderTaskDocumentDto dto)
        {
            Id = dto.Id;
            CreatedAt = dto.CreatedAt;
            UpdatedAt = dto.UpdatedAt;
            IsActive = dto.IsActive;
            Name = dto.Name;
            Url = dto.Url;
            ServiceOrderTaskId = dto.ServiceOrderTaskId;
            DocumentTypeId = dto.DocumentTypeId;
        }

        public static ServiceOrderTaskDocumentDto ToDto(ServiceOrderTaskDocument entity)
        {
            if (entity is null)
                return null!;

            return new ServiceOrderTaskDocumentDto
            {
                Id = entity.Id,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsActive = entity.IsActive,
                Name = entity.Name,
                Url = entity.Url,
                ServiceOrderTaskId = entity.ServiceOrderTaskId,
                DocumentTypeId = entity.DocumentTypeId
            };
        }
    }
}
