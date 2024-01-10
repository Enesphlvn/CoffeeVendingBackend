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
        public DayOfWeekStatistics Sunday { get; set; }
        public DayOfWeekStatistics Monday { get; set; }
        public DayOfWeekStatistics Tuesday { get; set; }
        public DayOfWeekStatistics Wednesday { get; set; }
        public DayOfWeekStatistics Thursday { get; set; }
        public DayOfWeekStatistics Friday { get; set; }
        public DayOfWeekStatistics Saturday { get; set; }
        public List<LowStockGeneralContentStatistics> LowStockGeneralContent { get; set; }
        public List<string> OutOfStockProducts { get; set; }
    }
}
