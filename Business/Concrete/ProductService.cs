using AutoMapper;
using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation.Product;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.Product;

namespace Business.Concrete
{
    public class ProductService : IProductService
    {
        public readonly IMapper _mapper;
        IProductDal _productDal;

        public ProductService(IProductDal productDal, IMapper mapper)
        {
            _productDal = productDal;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(CreateProductValidator))]
        public IResult Add(CreateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult HardDelete(int productId)
        {
            Product product = _productDal.Get(p => p.Id == productId);
            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleteFromDatabase);
        }

        public IDataResult<List<GetProductDto>> GetAll()
        {
            List<Product> products = _productDal.GetAll();

            List<GetProductDto> productDtos = _mapper.Map<List<GetProductDto>>(products);

            return new SuccessDataResult<List<GetProductDto>>(productDtos, Messages.ProductsListed);
        }

        public IDataResult<Product> GetById(int productId)
        {
            var result = _productDal.Get(P => P.Id == productId);

            if (result is null)
            {
                return new ErrorDataResult<Product>(Messages.ProductIsNull);
            }
            return new SuccessDataResult<Product>(result, Messages.ProductIdListed);
        }

        [ValidationAspect(typeof(UpdateProductValidator))]
        public IResult Update(UpdateProductDto productDto)
        {
            var product = _productDal.Get(p => p.Id == productDto.Id);

            if (product is null)
            {
                return new ErrorResult(Messages.ProductIsNull);
            }

            _mapper.Map(productDto, product);

            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        public IResult Delete(int productId)
        {
            Product product = _productDal.Get(p => p.Id == productId);

            if (product is null)
            {
                return new ErrorResult();
            }

            product.IsStatus = false;

            _productDal.Update(product);

            return new SuccessResult(Messages.ProductDeleted);
        }
    }
}
