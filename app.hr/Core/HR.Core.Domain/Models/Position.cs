namespace HR.Core.Domain.Models
{
    public class Position : AuditableEntity
    {
        public int Id { get; set; }
        public string PositionName { get; set; }
        public int SortId { get; set; }
    }
}
