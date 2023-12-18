using AutoMapper;
using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation.Order;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
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
        IGeneralContentDal _generalContentDal;
        IProductContentDal _productContentDal;
        IProductDal _productDal;
        IOrderDal _orderDal;
        IUserDal _userDal;

        public OrderService(IOrderDal orderDal, IProductDal productDal, IMapper mapper, IUserDal userDal, IProductContentDal productContentDal, IGeneralContentDal generalContentDal)
        {
            _generalContentDal = generalContentDal;
            _productContentDal = productContentDal;
            _productDal = productDal;
            _orderDal = orderDal;
            _mapper = mapper;
            _userDal = userDal;
        }

        [ValidationAspect(typeof(CreateOrderValidator))]
        public IResult Add(CreateOrderDto orderDto)
        {
            IResult userExists = BusinessRules.Run(CheckIfUserIdExists(orderDto.UserId), CheckIfProductIdExists(orderDto.ProductId));
            if (!userExists.Success)
            {
                return new ErrorResult(userExists.Message);
            }

            var order = _mapper.Map<Order>(orderDto);

            var calculateResult = CalculateRefundPaid(order.ProductId, order.AmountPaid);
            if(!calculateResult.Success)
            {
                return new ErrorResult(calculateResult.Message);
            }

            _orderDal.Add(order);

            var products = _productDal.GetAll(p => p.Id == order.ProductId).ToList();

            var updateResult = UpdateStockForOrderProducts(products);

            if (!updateResult.Success)
            {
                return new ErrorResult(updateResult.Message);
            }
            return new SuccessResult(Messages.OrderCompleted + $" Para üstünüz: {order.RefundPaid}₺.");
        }

        public IResult HardDelete(int orderId)
        {
            Order order = _orderDal.Get(o => o.Id == orderId);
            if (order is null)
            {
                return new ErrorResult(Messages.NoDataOnThisId);
            }
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

            IResult result = BusinessRules.Run(CheckIfProductIdExists(order.ProductId), CheckIfUserIdExists(order.UserId));
            if (!result.Success)
            {
                return new ErrorResult(result.Message);
            }

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
            List<Order> orders = _orderDal.GetAll(o => o.IsStatus);

            List<GetOrderDto> orderDtos = _mapper.Map<List<GetOrderDto>>(orders);

            return new SuccessDataResult<List<GetOrderDto>>(orderDtos, Messages.OrdersListed);
        }

        public IDataResult<List<GetOrderDetailDto>> GetOrderDetails()
        {
            return new SuccessDataResult<List<GetOrderDetailDto>>(_orderDal.GetOrderDetails(), Messages.OrdersListed);
        }

        private IResult CheckIfUserIdExists(int userId)
        {
            var checkUser = _userDal.Get(u => u.Id == userId);
            if (checkUser is null)
            {
                return new ErrorResult(Messages.UserIsNull);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductIdExists(int productId)
        {
            var checkProduct = _productDal.Get(o => o.Id == productId);
            if (checkProduct is null)
            {
                return new ErrorResult(Messages.ProductIsNull);
            }
            return new SuccessResult();
        }

        private IResult UpdateStockForOrderProducts(List<Product> products)
        {
            foreach (var product in products)
            {
                UpdateStockForProduct(product);
            }
            return new SuccessResult();
        }

        private IResult UpdateStockForProduct(Product product)
        {
            var productContents = _productContentDal.GetAll(pc => pc.ProductId == product.Id);
            foreach (var productContent in productContents)
            {
                UpdateStockForProductContent(productContent);
            }
            return new SuccessResult();
        }

        private IResult UpdateStockForProductContent(ProductContent productContent)
        {
            var generalContent = _generalContentDal.Get(gc => gc.Id == productContent.GeneralContentId);
            if(generalContent is null)
            {
                return new ErrorResult(Messages.GeneralContentNotFoundForProduct);
            }

            if(generalContent.Value < productContent.Unit)
            {
                return new ErrorResult(Messages.NoGeneralContentInStock);
            }

            if(generalContent.Value < 500)
            {
                generalContent.IsCritialLevel = true;
            }

            generalContent.Value -= productContent.Unit;
            _generalContentDal.Update(generalContent);
            return new SuccessResult();
        }

        private IResult CalculateRefundPaid(int productId, int amountPaid)
        {
            int productPrice = _productDal.Get(p => p.Id == productId)?.Price ?? 0;
            if(amountPaid < productPrice)
            {
                return new ErrorResult(Messages.AmountPaidInsufficient);
            }
            int refundPaid = amountPaid - productPrice;
            return new SuccessResult();
        }
    }
}