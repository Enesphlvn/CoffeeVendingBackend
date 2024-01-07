namespace Entities.DTOs.Statistics
{
    public class StatisticsDto
    {
        public decimal DailyRevenue { get; set; }
        public decimal MonthlyRevenue { get; set; }
        public decimal WeeklyRevenue { get; set; }
        public int WeeklyOrderCount { get; set; }
        public int DailyOrderCount { get; set; }
        public int MonthlyOrderCount { get; set; }
        public List<string> TopSoldProduct { get; set; }
        public List<string> LeastSoldProduct { get; set; }
        public List<string> TopOrderingUserNames { get; set; }
        public List<int> BusiestOrderHours { get; set; }
        public List<string> BusiestOrderDaysOfWeek { get; set; }
        public List<string> LowStockGeneralContent { get; set; }
        public List<string> OutOfStockProducts { get; set; }
    }
}
