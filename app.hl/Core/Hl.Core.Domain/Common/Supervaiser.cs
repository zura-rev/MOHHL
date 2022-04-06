using System;

namespace Hl.Core.Domain.Common
{
    public class Supervaiser
    {
        public string Name { get; set; }
        public int Count { get; set; }
        public DateTime? Expiration { get; set; }
        public bool IsActive { get; set; } 
    }
}
