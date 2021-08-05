using System;
using System.Collections.Generic;
using HL.Core.Domain.Models;

namespace HL.Core.Application.DTOs
{
    public class GetUserDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PrivateNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public HashSet<Resource> Resources = new HashSet<Resource>();

        public GetUserDto(User user) {
            Id = user.Id;
            UserName = user.UserName;
            PrivateNumber = user.PrivateNumber;
            FirstName = user.FirstName;
            LastName = user.LastName;

            foreach (var item in user.Resources)
            {
                Resources.Add(item);
            }
           
        }
    }
}
