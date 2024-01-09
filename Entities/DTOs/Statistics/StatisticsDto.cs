using Core.Entities.Abstract;

namespace Entities.DTOs.Statistics
{
    public class StatisticsDto : IDto
    {
        public decimal DailyRevenue { get; set; }
        public decimal MonthlyRevenue { get; set; }
        public decimal WeeklyRevenue { get; set; }
        public int WeeklyOrderCount { get; set; }
        public int DailyOrderCount { get; set; }
        public int MonthlyOrderCount { get; set; }
        public ProductStatistics TopSoldProduct { get; set; }
        public ProductStatistics LeastSoldProduct { get; set; }
        public UserStatistics TopOrderingUser { get; set; }
        public UserStatistics LeastOrderingUser { get; set; }
        public int TopOrderingHour { get; set; }
        public int LeastOrderingHour { get; set; }
        public string TopOrderingDayOfWeek { get; set; }
        public string LeastOrderingDayOfWeek { get; set; }
        public List<string> LowStockGeneralContent { get; set; }
        public List<string> OutOfStockProducts { get; set; }
    }


}
