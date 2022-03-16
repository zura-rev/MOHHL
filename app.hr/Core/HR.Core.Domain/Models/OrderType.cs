namespace HR.Core.Domain.Models
{
    public class OrderType : AuditableEntity
    {
        public int Id { get; set; }
        public string OrderTypeName { get; set; }
    }
}
