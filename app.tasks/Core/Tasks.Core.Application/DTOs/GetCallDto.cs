using Tasks.Core.Domain.Models;
using System;
using System.Collections.Generic;

namespace Tasks.Core.Application.DTOs
{
    public class GetCallDto
    {
        public int Id { get; set; }
        public string CallAuthor { get; set; }
        public string PrivateNumber { get; set; }
        public string Phone { get; set; }
        public GetCategoryDto Category { get; set; }
        public string Note { get; set; }
        public int CallType { get; set; }
        public DateTime CreateDate { get; set; }
        public User User { get; set; }
        public string Supervaiser { get; set; }
        public Card Card { get; set; }
    }
}
