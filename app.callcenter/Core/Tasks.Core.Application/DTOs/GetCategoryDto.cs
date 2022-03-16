using System;
using System.Collections.Generic;
using System.Text;

namespace Tasks.Core.Application.DTOs
{
    public class GetCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int ParentId { get; set; }
    }
}
