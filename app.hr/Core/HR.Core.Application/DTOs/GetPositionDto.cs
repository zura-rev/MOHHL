using System;

namespace HR.Core.Application.DTOs
{
    public class GetPositionDto
    {
        public int Id { get; set; }
        public string PositionName { get; set; }
        public int SortId { get; set; }
    }
}
