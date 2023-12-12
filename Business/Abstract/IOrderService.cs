using Core.Utilities.Results;
using Entities.DTOs.Order;

namespace Business.Abstract
{
    public interface IOrderService
    {
        IDataResult<List<GetOrderDto>> GetAll();
        IDataResult<List<GetOrderDetailDto>> GetOrderDetails();
        IResult Add(CreateOrderDto orderDto);
        IResult Update(UpdateOrderDto orderDto);
        IResult Delete(int orderId);
        IResult HardDelete(int orderId);
        IDataResult<GetOrderByIdDto> GetById(int orderId);
    }
}
