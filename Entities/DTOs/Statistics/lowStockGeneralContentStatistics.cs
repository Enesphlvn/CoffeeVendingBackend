using Core.Entities.Abstract;

namespace Entities.DTOs.Statistics
{
    public class LowStockGeneralContentStatistics : IDto
    {
        public string GeneralContentName { get; set; }
        public int Value { get; set; }
        public string Type { get; set; }
    }
}
