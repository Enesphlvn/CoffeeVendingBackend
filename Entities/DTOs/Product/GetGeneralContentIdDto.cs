using Core.Entities.Abstract;

namespace Entities.DTOs.Product
{
    public class GetGeneralContentIdDto : IDto
    {
        public int GeneralContentId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Price { get; set; }
        public string ImagePath { get; set; }
    }
}