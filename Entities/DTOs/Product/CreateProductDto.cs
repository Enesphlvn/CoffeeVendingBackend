using Core.Entities.Abstract;

namespace Entities.DTOs.Product
{
    public class CreateProductDto : IDto
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public string ImagePath { get; set; }
    }
}
