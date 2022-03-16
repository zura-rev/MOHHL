using System.Collections.Generic;
using Tasks.Core.Domain.Models;

namespace Tasks.Core.Application.DTOs
{
    public class GetUserDto
    {

        public GetUserDto() 
        {
            Resources = new HashSet<Resource>();
        }

        public int Id { get; set; }
        public string UserName { get; set; }
        public string PrivateNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public ICollection<Resource> Resources { get; set; }

        //public GetUserDto(User user) {
        //    Id = user.Id;
        //    UserName = user.UserName;
        //    PrivateNumber = user.PrivateNumber;
        //    FirstName = user.FirstName;
        //    LastName = user.LastName;

        //    foreach (var item in user.Resources)
        //    {
        //        Resources.Add(item);
        //    }
        //}
    }
}
