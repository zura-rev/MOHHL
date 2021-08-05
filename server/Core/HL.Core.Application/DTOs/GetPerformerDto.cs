﻿using HL.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HL.Core.Application.DTOs
{
    public class GetPerformerDto
    {
        public int Id { get; set; }
        public int CallId { get; set; }
        public Call Call { get; set; }
        public int UserId { get; set; }
        //public User User { get; set; }
        public int UserType { get; set; }
        public int Status { get; set; }
        public DateTime? PerformDate { get; set; }
        public string Note { get; set; }
    }
}