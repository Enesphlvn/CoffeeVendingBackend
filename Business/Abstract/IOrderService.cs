using Core.Utilities.Results;
using Entities.DTOs.Order;

namespace Business.Abstract
{
    public interface IOrderService
    {
        IDataResult<List<GetAllOrderDto>> GetAll();
        IResult Add(CreateOrderDto orderDto);
        IResult Update(UpdateOrderDto orderDto);
        IResult Delete(int orderId);
        IResult HardDelete(int orderId);
        IDataResult<GetOrderByIdDto> GetById(int orderId);
    }
}
