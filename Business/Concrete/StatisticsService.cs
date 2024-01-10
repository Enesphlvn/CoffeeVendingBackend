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
                    Sunday = Sunday(),
                    Monday = Monday(),
                    Tuesday = Tuesday(),
                    Wednesday = Wednesday(),
                    Thursday = Thursday(),
                    Friday = Friday(),
                    Saturday = Saturday(),
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

        private DayOfWeekStatistics Sunday()
        {
            return CreateDayOfWeekStatistics(DayOfWeek.Sunday);
        }

        private DayOfWeekStatistics Monday()
        {
            return CreateDayOfWeekStatistics(DayOfWeek.Monday);
        }

        private DayOfWeekStatistics Tuesday()
        {
            return CreateDayOfWeekStatistics(DayOfWeek.Tuesday);
        }

        private DayOfWeekStatistics Wednesday()
        {
            return CreateDayOfWeekStatistics(DayOfWeek.Wednesday);
        }

        private DayOfWeekStatistics Thursday()
        {
            return CreateDayOfWeekStatistics(DayOfWeek.Thursday);
        }

        private DayOfWeekStatistics Friday()
        {
            return CreateDayOfWeekStatistics(DayOfWeek.Friday);
        }

        private DayOfWeekStatistics Saturday()
        {
            return CreateDayOfWeekStatistics(DayOfWeek.Saturday);
        }

        private List<LowStockGeneralContentStatistics> LowStockGeneralContent()
        {
            List<LowStockGeneralContentStatistics> lowStockGeneralContents = new List<LowStockGeneralContentStatistics>();

            var lowStockGeneralContentsQuery = _generalContentDal.GetAll(gc => gc.Value < gc.IsCritialLevelValue);

            foreach (var generalContent in lowStockGeneralContentsQuery)
            {
                lowStockGeneralContents.Add(new LowStockGeneralContentStatistics
                {
                    GeneralContentName = generalContent.Name,
                    Value = generalContent.Value,
                    Type = generalContent.Type
                });
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

        private DayOfWeekStatistics CreateDayOfWeekStatistics(DayOfWeek dayOfWeek)
        {
            var orders = _orderDal.GetAll(o => o.CreatedAt.DayOfWeek == dayOfWeek);
            return new DayOfWeekStatistics
            {
                DayName = CultureInfo.CurrentCulture.DateTimeFormat.GetDayName(dayOfWeek),
                Quantity = orders.Count()
            };
        }
    }
}
