using Core.Entities.Abstract;

namespace Entities.DTOs.Product
{
    public class GetProductByIdDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public string ImagePath { get; set; }
    }
}
