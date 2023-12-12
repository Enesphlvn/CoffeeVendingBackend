using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.Order;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderDal : EfEntityRepositoryBase<Order, CoffeeVendingContext>, IOrderDal
    {
        public List<GetOrderDetailDto> GetOrderDetails()
        {
            using (CoffeeVendingContext context = new CoffeeVendingContext())
            {
                var result = from o in context.Orders
                             join p in context.Products
                             on o.ProductId equals p.Id
                             select new GetOrderDetailDto
                             {
                                 Id = o.Id,
                                 ProductName = p.Name,
                                 AmountPaid = o.AmountPaid,
                                 RefundPaid = o.RefundPaid
                             };
                return result.ToList();
            }
        }
    }
}
