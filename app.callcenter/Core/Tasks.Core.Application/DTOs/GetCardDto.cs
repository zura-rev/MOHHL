using Tasks.Core.Domain.Models;
using System;

namespace Tasks.Core.Application.DTOs
{
    public class GetCardDto
    {
        public int Id { get; set; }
        public int CallId { get; set; }
        public Call Call { get; set; }
        public int UserId { get; set; }
        public int UserType { get; set; }
        public int Status { get; set; }
        public DateTime? PerformDate { get; set; }
        public string Note { get; set; }
    }
}
