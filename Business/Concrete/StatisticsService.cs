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
                    TopSoldProduct = TopSoldProduct(),
                    LeastSoldProduct = LeastSoldProduct(),
                    TopOrderingUser = TopOrderingUser(),
                    LeastOrderingUser = LeastOrderingUser(),
                    TopOrderingHour = TopOrderingHour(),
                    LeastOrderingHour = LeastOrderingHour(),
                    TopOrderingDayOfWeek = TopOrderingDayOfWeek(),
                    LeastOrderingDayOfWeek = LeastOrderingDayOfWeek(),
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

        private ProductStatistics TopSoldProduct()
        {
            var topSoldProduct = _orderDal.GetAll().GroupBy(o => o.ProductId)
                .OrderByDescending(g => g.Sum(o => o.AmountPaid)).FirstOrDefault();

            if (topSoldProduct != null)
            {
                return new ProductStatistics
                {
                    ProductName = GetProductName(topSoldProduct.Key),
                    Quantity = topSoldProduct.Count()
                };
            }

            return null;
        }

        private ProductStatistics LeastSoldProduct()
        {
            var topSoldProduct = _orderDal.GetAll().GroupBy(o => o.ProductId)
                .OrderBy(g => g.Sum(o => o.AmountPaid)).FirstOrDefault();

            if (topSoldProduct != null)
            {
                return new ProductStatistics
                {
                    ProductName = GetProductName(topSoldProduct.Key),
                    Quantity = topSoldProduct.Count()
                };
            }

            return null;
        }

        private UserStatistics TopOrderingUser()
        {
            var topOrderingUser = _orderDal.GetAll().Where(o => o.IsStatus).GroupBy(o => o.UserId)
                .OrderByDescending(g => g.Count()).FirstOrDefault();

            if (topOrderingUser != null)
            {
                return new UserStatistics
                {
                    UserName = GetUserName(topOrderingUser.Key),
                    Quantity = topOrderingUser.Count()
                };
            }

            return null;
        }

        private UserStatistics LeastOrderingUser()
        {
            var leastOrderingUser = _orderDal.GetAll().Where(o => o.IsStatus).GroupBy(o => o.UserId)
                .Where(g => g.Key != 0)
                .OrderBy(g => g.Count()).ThenBy(g => g.Key).FirstOrDefault();

            if (leastOrderingUser != null)
            {
                var userName = GetUserName(leastOrderingUser.Key);

                return new UserStatistics
                {
                    UserName = string.IsNullOrEmpty(userName) ? "Bilinmeyen Kullanıcı" : userName,
                    Quantity = leastOrderingUser.Count()
                };
            }

            return null;
        }




        private int TopOrderingHour()
        {
            var topOrderingHour = _orderDal.GetAll().GroupBy(o => o.CreatedAt.Hour)
                .OrderByDescending(g => g.Count()).FirstOrDefault();

            return topOrderingHour != null ? topOrderingHour.Key : 0;
        }

        private int LeastOrderingHour()
        {
            var leastOrderingHour = _orderDal.GetAll().GroupBy(o => o.CreatedAt.Hour)
                .OrderBy(g => g.Count()).FirstOrDefault();

            return leastOrderingHour != null ? leastOrderingHour.Key : 0;
        }

        private string TopOrderingDayOfWeek()
        {
            var topOrderingDayOfWeek = _orderDal.GetAll().GroupBy(o => o.CreatedAt.DayOfWeek)
                .OrderByDescending(g => g.Count()).FirstOrDefault();

            return topOrderingDayOfWeek != null ? CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(topOrderingDayOfWeek.Key) : string.Empty;
        }

        private string LeastOrderingDayOfWeek()
        {
            var leastOrderingDayOfWeek = _orderDal.GetAll().GroupBy(o => o.CreatedAt.DayOfWeek)
                .OrderBy(g => g.Count()).FirstOrDefault();

            return leastOrderingDayOfWeek != null ? CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(leastOrderingDayOfWeek.Key) : string.Empty;
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

        private string GetProductName(int productId)
        {
            var product = _productDal.Get(p => p.Id == productId);

            return product != null ? product.Name : string.Empty;
        }

        private string GetUserName(int userId)
        {
            var user = _userDal.Get(p => p.Id == userId);

            return user != null ? (user.FirstName + " " + user.LastName) : string.Empty;
        }
    }

}
