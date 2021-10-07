using HR.Core.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Core.Application.DTOs
{
    public class SetEmployeeDto
    {
        public string PrivateNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public SetPositionDto Position { get; set; }
    }
}
