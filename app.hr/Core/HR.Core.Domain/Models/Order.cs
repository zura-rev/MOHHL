using System;

namespace HR.Core.Domain.Models
{
    public class Order : AuditableEntity
    {
        public int Id { get; set; }
        public OrderType OrderType { get; set; }
        public Employee Employee { get; set; }
        public Structure Structure { get; set; }
        public double RealSalary { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
