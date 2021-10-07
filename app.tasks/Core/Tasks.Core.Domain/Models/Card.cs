using System;
using System.Collections.Generic;
using System.Text;

namespace Tasks.Core.Domain.Models
{
    public class Card
    {
        public int Id { get; set; }
        public Call Call { get; set; }
        public User User { get; set; }
        public int UserType { get; set; }
        public int Status { get; set; }
        public DateTime? PerformDate { get; set; }
        public string Note { get; set; }
    }
}
