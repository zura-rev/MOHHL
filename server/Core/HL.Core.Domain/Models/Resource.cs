using System.Collections.Generic;

namespace HL.Core.Domain.Models
{
    public class Resource
    {
        public Resource(int Id, string Name, string Description)
        {
            this.Id = Id;
            this.Name = Name;
            this.Description = Description;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<User> Users { get; set; }

    }
}
