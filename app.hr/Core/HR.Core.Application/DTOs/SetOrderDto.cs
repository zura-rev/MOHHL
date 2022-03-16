using HR.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Core.Application.DTOs
{
    public class SetOrderDto
    {
        public OrderType OrderType { get; set; }
        public Employee Employee { get; set; }
        public Structure Structure { get; set; }
        public double RealSalary { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
