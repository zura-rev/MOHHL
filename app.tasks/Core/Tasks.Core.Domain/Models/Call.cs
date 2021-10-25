using System;
using System.Collections.Generic;

namespace Tasks.Core.Domain.Models
{
    public class Call //: AuditableEntity
    {
        public int Id { get; set; }
        public string CallAuthor { get; set; }
        public string PrivateNumber { get; set; }
        public string Phone { get; set; }
        public Category Category { get; set; }
        public string Note { get; set; }
        public DateTime CreateDate { get; set; }
        public int CallType { get; set; }
        public User User { get; set; }
        public Card Card { get; set; }
    }
}
