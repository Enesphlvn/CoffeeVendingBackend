using Core.Entities.Abstract;

namespace Entities.DTOs.ProductContent
{
    public class GetAllProductContentDto : IDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int GeneralContentId { get; set; }
        public string GeneralContentName { get; set; }
        public int Unit { get; set; }
    }
}
