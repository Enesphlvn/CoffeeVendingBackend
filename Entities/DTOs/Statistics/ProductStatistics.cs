using Core.Entities.Abstract;

namespace Entities.DTOs.Statistics
{
    public class ProductStatistics : IDto
    {
        public string ProductName { get; set; }
        public int Quantity { get; set; }
    }
}
