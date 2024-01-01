using Core.DataAccess;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.Order;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOrderDal : EfEntityRepositoryBase<Order, CoffeeVendingContext>, IOrderDal
    {
        public List<GetAllOrderDto> GetOrderDetails()
        {
            using (CoffeeVendingContext context = new CoffeeVendingContext())
            {
                var result = from o in context.Orders
                             join p in context.Products
                             on o.ProductId equals p.Id
                             join u in context.Users
                             on o.UserId equals u.Id
                             where o.IsStatus == true
                             select new GetAllOrderDto
                             {
                                 Id = o.Id,
                                 ProductId = p.Id,
                                 ProductName = p.Name,
                                 UserId = u.Id,
                                 UserName = (u.FirstName + " " + u.LastName),
                                 AmountPaid = o.AmountPaid,
                                 RefundPaid = o.RefundPaid,
                             };
                return result.ToList();
            }
        }
    }
}