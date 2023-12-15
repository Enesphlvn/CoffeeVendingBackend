using Core.Entities.Abstract;

namespace Entities.DTOs.ProductContent
{
    public class UpdateProductContentDto : IDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int GeneralContentId { get; set; }
        public int Unit { get; set; }
    }
}
