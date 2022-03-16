using System.Collections.Generic;

namespace Tasks.Core.Domain.Models
{
    public class Resource
    {
        public Resource()
        {
            Users = new List<User>();
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<User> Users { get; set; }

    }
}
