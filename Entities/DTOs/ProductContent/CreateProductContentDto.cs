using Core.Entities.Abstract;

namespace Entities.DTOs.ProductContent
{
    public class CreateProductContentDto : IDto
    {
        public int ProductId { get; set; }
        public int GeneralContentId { get; set; }
        public int Unit { get; set; }
    }
}
