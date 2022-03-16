using System;

namespace HR.Core.Application.DTOs
{
    public class GetEmployeeDto
    {
        public int Id { get; set; }
        public string PrivateNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime BirthDate { get; set; }
    }
}
