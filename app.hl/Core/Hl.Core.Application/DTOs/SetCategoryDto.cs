namespace Hl.Core.Application.DTOs
{
    public class SetCategoryDto
    {
        public int CategoryName { get; set; }
        public int ParentId { get; set; }
        public int Status { get; set; }
        public string Note { get; set; }
    }
}
