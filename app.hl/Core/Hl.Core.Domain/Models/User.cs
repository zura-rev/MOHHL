using System;
using System.Collections.Generic;

namespace Hl.Core.Domain.Models
{
    public class User //: AuditableEntity
    {
        public User()
        {
            Resources = new List<Resource>();
        }
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string PrivateNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool ResetPassword { get; set; }
        public virtual ICollection<Resource> Resources { get; set; }
    }
}
