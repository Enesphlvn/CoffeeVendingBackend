using AutoMapper;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
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
            IResult idExists = BusinessRules.Run(CheckIfUserIdExists(orderDto.UserId), CheckIfProductIdExists(orderDto.ProductId));
            if (idExists != null)
            {
                return new ErrorResult(idExists.Message);
            }

            var order = _mapper.Map<Order>(orderDto);

            var calculateResult = CalculateRefundPaid(order.ProductId, order.AmountPaid);
            if (!calculateResult.Success)
            {
                return new ErrorResult(calculateResult.Message);
            }

            order.RefundPaid = calculateResult.Data;

            var products = _productDal.GetAll(p => p.Id == order.ProductId).ToList();

            var updateResult = UpdateStockForOrderProducts(products);
            if (!updateResult.Success)
            {
                return new ErrorResult(updateResult.Message);
            }

            _orderDal.Add(order);

            return new SuccessResult($" Para üstünüz: {order.RefundPaid}₺");
        }

        [SecuredOperation("admin")]
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

        [SecuredOperation("admin")]
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

        [SecuredOperation("admin")]
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

        public IDataResult<List<GetAllOrderDto>> GetAll()
        {
            return new SuccessDataResult<List<GetAllOrderDto>>(_orderDal.GetOrderDetails(), Messages.OrdersListed);
        }

        private IResult CheckIfUserIdExists(int userId)
        {
            var checkUser = _userDal.Get(u => u.Id == userId);
            if (checkUser is null || checkUser.IsStatus == false)
            {
                return new ErrorResult(Messages.UserIsNull);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductIdExists(int productId)
        {
            var checkProduct = _productDal.Get(o => o.Id == productId);
            if (checkProduct is null || checkProduct.IsStatus == false)
            {
                return new ErrorResult(Messages.ProductIsNull);
            }
            return new SuccessResult();
        }

        private IResult UpdateStockForOrderProducts(List<Product> products)
        {
            foreach (var product in products)
            {
                var result = UpdateStockForProduct(product);
                if (!result.Success)
                {
                    return new ErrorResult(result.Message);
                }
            }
            return new SuccessResult();
        }

        private IResult UpdateStockForProduct(Product product)
        {
            var productContents = _productContentDal.GetAll(pc => pc.ProductId == product.Id);
            foreach (var productContent in productContents)
            {
                var result = UpdateStockForProductContent(productContent);
                if(!result.Success)
                {
                    return new ErrorResult(result.Message);
                }
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
                return new ErrorResult($"{generalContent.Name} tükendi.");
            }

            generalContent.Value -= productContent.Unit;

            if (generalContent.Value < generalContent.IsCritialLevelValue)
            {
                generalContent.IsCritialLevel = true;
            }
            else
            {
                generalContent.IsCritialLevel = false;
            }

            _generalContentDal.Update(generalContent);
            return new SuccessResult();
        }

        private IDataResult<int> CalculateRefundPaid(int productId, int amountPaid)
        {
            int productPrice = _productDal.Get(p => p.Id == productId)?.Price ?? 0;
            if (amountPaid < productPrice)
            {
                return new ErrorDataResult<int>(Messages.AmountPaidInsufficient);
            }
            int refundPaid = amountPaid - productPrice;
            return new SuccessDataResult<int>(refundPaid);
        }
    }
}