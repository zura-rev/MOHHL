﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Tasks.Core.Application.DTOs
{
    public class SetCategoryDto
    {
        public int CategoryName { get; set; }
        public int ParentId { get; set; }
    }
}