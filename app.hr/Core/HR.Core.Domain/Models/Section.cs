namespace HR.Core.Domain.Models
{
    public class Section : AuditableEntity
    {
        public int Id { get; set; }
        public string SectionName { get; set; }
        public int ParentId { get; set; }
    }
}
