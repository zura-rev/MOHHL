using System;
using System.Collections.Generic;

namespace Tasks.Core.Domain.Models
{
    public class User: AuditableEntity
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PrivateNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public ICollection<Resource> Resources { get; set; }

    }
}
