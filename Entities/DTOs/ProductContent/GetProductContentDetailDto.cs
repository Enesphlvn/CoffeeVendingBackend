using Core.Entities.Abstract;

namespace Entities.DTOs.ProductContent
{
    public class GetProductContentDetailDto : IDto
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string GeneralContentName { get; set; }
        public int Unit { get; set; }
    }
}
