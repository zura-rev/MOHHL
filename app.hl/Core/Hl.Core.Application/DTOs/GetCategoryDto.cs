namespace Hl.Core.Application.DTOs
{
    public class GetCategoryDto
    {
        public int Id { get; set; }
        public string CategoryName { get; set; }
        public int ParentId { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }
}
