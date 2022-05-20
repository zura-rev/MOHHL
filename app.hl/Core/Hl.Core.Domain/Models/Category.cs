using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Hl.Core.Domain.Models
{
    public class Category //: AuditableEntity
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int ParentId { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }
}
