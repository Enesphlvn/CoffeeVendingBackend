using Core.Entities.Abstract;

namespace Entities.DTOs.Statistics
{
    public class DayOfWeekStatistics : IDto
    {
        public string DayName { get; set; }
        public int Quantity { get; set; }
    }
}
