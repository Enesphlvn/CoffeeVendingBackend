using Core.Entities.Abstract;
using System.Text.Json.Serialization;

namespace Entities.DTOs.Product
{
    public class UpdateProductDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Price { get; set; }
        public string ImagePath { get; set; }
    }
}
