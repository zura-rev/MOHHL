using HR.Core.Domain.Enums;
using System;

namespace HR.Core.Domain.Models
{
    public class Employee : AuditableEntity
    {
        public int Id { get; set; }
        public string PrivateNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public string Image { get; set; }
    }
}
