namespace HR.Core.Domain.Models
{
    public class Structure : AuditableEntity
    {
        public int Id { get; set; }
        public Section Section { get; set; }
        public Position Position { get; set; }
        public double DefaultSalary { get; set; }
        public int Count { get; set; }
    }
}
