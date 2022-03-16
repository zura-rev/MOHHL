using System;

namespace Tasks.Core.Domain.Common
{
    public abstract class AuditableEntity
    {
        public int UserId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public DateTime? DateDeleted { get; set; }
    }
}
