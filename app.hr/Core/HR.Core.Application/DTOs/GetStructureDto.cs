﻿using HR.Core.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Core.Application.DTOs
{
    public class GetStructureDto
    {
        public int Id { get; set; }
        public Section Section { get; set; }
        public Position Position { get; set; }
        public double DefaultSalary { get; set; }
        public int Count { get; set; }
    }
}