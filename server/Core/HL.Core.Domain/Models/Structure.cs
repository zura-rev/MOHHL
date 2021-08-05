using System;
using System.Collections.Generic;
using System.Text;

namespace HL.Core.Domain.Models
{
    public class Structure : AuditableEntity
    {
        public int Id { get; set; }
        public int StructureName { get; set; }
        public int ParentId { get; set; }
    }
}
