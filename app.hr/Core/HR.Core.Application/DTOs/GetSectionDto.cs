using HR.Core.Domain.Models;
using System;

namespace HR.Core.Application.DTOs
{
 

    public class GetSectionDto : AuditableEntity
    {
        public int Id { get; set; }
        public string SectionName { get; set; }
        public int ParentId { get; set; }
    }
}
