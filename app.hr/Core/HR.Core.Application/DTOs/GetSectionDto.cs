using System;
using System.Collections.Generic;
using System.Text;

namespace HR.Core.Application.DTOs
{
    public  class GetSectionDto
    {
        public int Id { get; set; }
        public int SectionName { get; set; }
        public int ParentId { get; set; }
    }
}
