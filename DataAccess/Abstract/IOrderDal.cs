using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs.Order;

namespace DataAccess.Abstract
{
    public interface IOrderDal : IEntityRepository<Order>
    {
        List<GetAllOrderDto> GetOrderDetails();
    }
}
