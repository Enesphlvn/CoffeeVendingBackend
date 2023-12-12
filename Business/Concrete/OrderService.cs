using AutoMapper;
using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation.Order;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.Order;
using Entities.DTOs.Product;

namespace Business.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IMapper _mapper;
        IOrderDal _orderDal;
        IProductDal _productDal;

        public OrderService(IOrderDal orderDal, IProductDal productDal, IMapper mapper)
        {
            _orderDal = orderDal;
            _productDal = productDal;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(CreateOrderValidator))]
        public IResult Add(CreateOrderDto orderDto)
        {
            var order = _mapper.Map<Order>(orderDto);

            _orderDal.Add(order);
            return new SuccessResult(Messages.OrderAdded);
        }

        public IResult HardDelete(int orderId)
        {
            Order order = _orderDal.Get(o => o.Id == orderId);
            _orderDal.Delete(order);
            return new SuccessResult(Messages.OrderDeleteFromDatabase);
        }

        public IDataResult<GetOrderByIdDto> GetById(int orderId)
        {
            var order = _orderDal.Get(o => o.Id == orderId);

            if (order is null)
            {
                return new ErrorDataResult<GetOrderByIdDto>(Messages.OrderIsNull);
            }

            var products = _productDal.GetAll(p => p.Id == order.ProductId);
            order.Products = products.ToList();

            var orderDto = _mapper.Map<GetOrderByIdDto>(order);

            orderDto.Products = _mapper.Map<ICollection<GetProductDto>>(products);

            return new SuccessDataResult<GetOrderByIdDto>(orderDto, Messages.OrderIdListed);
        }

        [ValidationAspect(typeof(UpdateOrderValidator))]
        public IResult Update(UpdateOrderDto orderDto)
        {
            var order = _orderDal.Get(o => o.Id == orderDto.Id);

            if (order is null)
            {
                return new ErrorResult(Messages.OrderIsNull);
            }

            _mapper.Map(orderDto, order);

            _orderDal.Update(order);
            return new SuccessResult(Messages.OrderUpdated);
        }

        public IResult Delete(int orderId)
        {
            Order order = _orderDal.Get(o => o.Id == orderId);
            if (order is null)
            {
                return new ErrorResult(Messages.OrderIsNull);
            }

            order.IsStatus = false;

            _orderDal.Update(order);

            return new SuccessResult(Messages.OrderDeleted);
        }

        public IDataResult<List<GetOrderDto>> GetAll()
        {
            List<Order> orders = _orderDal.GetAll();

            List<GetOrderDto> orderDtos = _mapper.Map<List<GetOrderDto>>(orders);

            return new SuccessDataResult<List<GetOrderDto>>(orderDtos, Messages.OrdersListed);
        }

        public IDataResult<List<GetOrderDetailDto>> GetOrderDetails()
        {
            return new SuccessDataResult<List<GetOrderDetailDto>>(_orderDal.GetOrderDetails(), Messages.OrdersListed);
        }
    }
}
