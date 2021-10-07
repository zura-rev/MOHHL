using System;

namespace HR.Core.Domain.Models
{
    public abstract class AuditableEntity
    {
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateDeleted { get; set; }
    }
}
