using AutoMapper;
using Business.Abstract;
using Business.Constans;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.DTOs.Statistics;
using System.Globalization;

namespace Business.Concrete
{
    public class StatisticsService : IStatisticsService
    {
        IGeneralContentDal _generalContentDal;
        IProductContentDal _productContentDal;
        IProductDal _productDal;
        IOrderDal _orderDal;
        IUserDal _userDal;

        public StatisticsService(IOrderDal orderDal, IProductDal productDal, IMapper mapper, IUserDal userDal, IProductContentDal productContentDal, IGeneralContentDal generalContentDal)
        {
            _generalContentDal = generalContentDal;
            _productContentDal = productContentDal;
            _productDal = productDal;
            _orderDal = orderDal;
            _userDal = userDal;
        }

        public IDataResult<StatisticsDto> GetAllStatistics()
        {
            try
            {
                StatisticsDto statistics = new StatisticsDto()
                {
                    DailyRevenue = DailyRevenue(),
                    DailyOrderCount = DailyOrderCount(),
                    MonthlyRevenue = MonthlyRevenue(),
                    MonthlyOrderCount = MonthlyOrderCount(),
                    WeeklyRevenue = WeeklyRevenue(),
                    WeeklyOrderCount = WeeklyOrderCount(),
                    TopSoldProduct = TopSoldProducts(),
                    LeastSoldProduct = LeastSoldProducts(),
                    TopOrderingUserNames = TopOrderingUsers(),
                    BusiestOrderHours = BusiestOrderHours(),
                    BusiestOrderDaysOfWeek = BusiestOrderDaysOfWeek(),
                    LowStockGeneralContent = LowStockGeneralContent(),
                    OutOfStockProducts = OutOfStockProducts(),
                };
                return new SuccessDataResult<StatisticsDto>(statistics, Messages.StatisticsListed);
            }
            catch (Exception)
            {
                return new ErrorDataResult<StatisticsDto>("İstatistikler alınırken bir hata oluştu");
            }
        }

        private decimal DailyRevenue()
        {
            DateTimeOffset today = DateTimeOffset.Now;

            var dailyOrders = _orderDal.GetAll(o => today.Date <= o.CreatedAt && today.Date.AddDays(1).AddSeconds(-1) > o.CreatedAt);

            decimal totalRevenue = dailyOrders.Sum(o => o.AmountPaid);

            return totalRevenue;
        }

        private int DailyOrderCount()
        {
            DateTimeOffset today = DateTimeOffset.Now;

            var dailyOrders = _orderDal.GetAll(o => today.Date <= o.CreatedAt && today.AddDays(1).AddSeconds(-1) > o.CreatedAt);

            int totalOrderCount = dailyOrders.Count();

            return totalOrderCount;
        }

        private decimal MonthlyRevenue()
        {
            DateTimeOffset lastMonth = DateTimeOffset.Now.AddMonths(-1);

            var monthlyOrders = _orderDal.GetAll(o => lastMonth.Date <= o.CreatedAt && lastMonth.Date.AddMonths(1).AddSeconds(-1) > o.CreatedAt);

            decimal totalRevenue = monthlyOrders.Sum(o => o.AmountPaid);

            return totalRevenue;
        }

        private int MonthlyOrderCount()
        {
            DateTimeOffset lastMonth = DateTimeOffset.Now.AddMonths(-1);

            var monthlyOrders = _orderDal.GetAll(o => lastMonth.Date <= o.CreatedAt && lastMonth.Date.AddMonths(1).AddSeconds(-1) > o.CreatedAt);

            int totalOrderCount = monthlyOrders.Count();

            return totalOrderCount;
        }

        private decimal WeeklyRevenue()
        {
            DateTimeOffset lastWeek = DateTimeOffset.Now.AddDays(-7);

            var weeklyOrders = _orderDal.GetAll(o => lastWeek.Date <= o.CreatedAt && lastWeek.Date.AddDays(7).AddSeconds(-1) > o.CreatedAt);

            decimal totalRevenue = weeklyOrders.Sum(o => o.AmountPaid);

            return totalRevenue;
        }

        private int WeeklyOrderCount()
        {
            DateTimeOffset lastWeek = DateTimeOffset.Now.AddDays(-7);

            var weeklyOrders = _orderDal.GetAll(o => lastWeek.Date <= o.CreatedAt && lastWeek.Date.AddDays(7).AddSeconds(-1) > o.CreatedAt);

            int totalRevenue = weeklyOrders.Count();

            return totalRevenue;
        }

        private List<string> TopSoldProducts()
        {
            var topSoldProducts = _orderDal.GetAll().GroupBy(o => o.ProductId)
                .OrderByDescending(g => g.Sum(o => o.AmountPaid))
                .Take(3)
                .Select(g => _productDal.Get(p => p.Id == g.Key))
                .Select(product => product != null ? product.Name : string.Empty)
                .ToList();

            return topSoldProducts;
        }

        private List<string> LeastSoldProducts()
        {
            var leastSoldProducts = _orderDal.GetAll().GroupBy(o => o.ProductId)
                .OrderBy(g => g.Sum(o => o.AmountPaid))
                .Take(3)
                .Select(g => _productDal.Get(p => p.Id == g.Key))
                .Select(product => product != null ? product.Name : string.Empty)
                .ToList();

            return leastSoldProducts;
        }

        private List<string> TopOrderingUsers()
        {
            var topOrderingUserNames = _orderDal.GetAll().GroupBy(o => o.UserId)
                .OrderByDescending(g => g.Count()).Take(3)
                .Select(g => _userDal.Get(u => u.Id == g.Key))
                .Select(user => user != null ? (user.FirstName + " " + user.LastName) : string.Empty)
                .ToList();

            return topOrderingUserNames;
        }

        private List<int> BusiestOrderHours()
        {
            var busiestOrderHours = _orderDal.GetAll().GroupBy(o => o.CreatedAt.Hour)
                .OrderByDescending(g => g.Count()).Take(2)
                .Select(g => g.Key)
                .ToList();

            return busiestOrderHours;
        }

        private List<string> BusiestOrderDaysOfWeek()
        {
            var busiestOrderDaysOfWeek = _orderDal.GetAll()
                .GroupBy(o => o.CreatedAt.DayOfWeek)
                .OrderByDescending(g => g.Count())
                .Take(3)
                .Select(g => g.Key)
                .Select(dayOfWeek => CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dayOfWeek))
                .ToList();

            return busiestOrderDaysOfWeek;
        }

        private List<string> LowStockGeneralContent()
        {
            List<string> lowStockGeneralContents = new List<string>();

            var lowStockGeneralContentsQuery = _generalContentDal.GetAll(gc => gc.Value < gc.IsCritialLevelValue);

            foreach (var generalContent in lowStockGeneralContentsQuery)
            {
                lowStockGeneralContents.Add(generalContent.Name);
            }

            return lowStockGeneralContents;
        }

        private List<string> OutOfStockProducts()
        {
            List<string> outOfStockProducts = new List<string>();

            var productContents = _productContentDal.GetAll();
            var generalContents = _generalContentDal.GetAll();

            foreach (var productContent in productContents)
            {
                var generalContent = generalContents.FirstOrDefault(gc => gc.Id == productContent.GeneralContentId);

                if (generalContent != null && generalContent.Value < productContent.Unit)
                {
                    var product = _productDal.Get(p => p.Id == productContent.ProductId);
                    if (product != null)
                    {
                        outOfStockProducts.Add(product.Name);
                    }
                }
            }

            return outOfStockProducts;
        }
    }

}
