using System;
using System.Collections.Generic;
using System.Text;

namespace Tasks.Core.Application.DTOs
{
    public class SetCallDto
    {
        public string CallAuthor { get; set; }
        public string PrivateNumber { get; set; }
        public SetCategoryDto Category { get; set; }
        public string Note { get; set; }
        public int CallStatus { get; set; }
    }
}
