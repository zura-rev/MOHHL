using System;

namespace HR.Core.Domain.Models
{
    public class Position: AuditableEntity
    {
        public int Id { get; set; }
        public string PositionName { get; set; }
        public double Salary { get; set; }
    }
}
