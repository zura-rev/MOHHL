using HL.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HL.Core.Domain.Models
{
    public class Employee: AuditableEntity
    {
        public int Id { get; set; }
        public string PrivateNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public Position Position { get; set; }
    }
}
