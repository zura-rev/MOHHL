namespace HL.Core.Domain.Models
{
    public class Vacancy : AuditableEntity
    {
        public int Id { get; set; }
        public Structure Structure { get; set; }
        public Position Position { get; set; }
        public Employee Employee { get; set; }
    }
}
