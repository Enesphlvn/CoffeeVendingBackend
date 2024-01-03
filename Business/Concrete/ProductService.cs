using AutoMapper;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Business.ValidationRules.FluentValidation.Product;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
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

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(CreateProductValidator))]
        public IResult Add(CreateProductDto productDto)
        {
            var product = _mapper.Map<Product>(productDto);

            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.Name));

            if (result != null)
            {
                return new ErrorResult(result.Message);
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        [SecuredOperation("admin")]
        public IResult HardDelete(int productId)
        {
            Product product = _productDal.Get(p => p.Id == productId);
            if (product is null)
            {
                return new ErrorResult(Messages.NoDataOnThisId);
            }

            _productDal.Delete(product);
            return new SuccessResult(Messages.ProductDeleteFromDatabase);
        }

        public IDataResult<List<GetProductDto>> GetAll()
        {
            List<Product> products = _productDal.GetAll(p => p.IsStatus);

            List<GetProductDto> productDtos = _mapper.Map<List<GetProductDto>>(products);

            return new SuccessDataResult<List<GetProductDto>>(productDtos, Messages.ProductsListed);
        }

        public IDataResult<GetProductByIdDto> GetById(int productId)
        {
            var product = _productDal.Get(p => p.Id == productId);

            if (product is null)
            {
                return new ErrorDataResult<GetProductByIdDto>(Messages.ProductIsNull);
            }

            var productDtos = _mapper.Map<GetProductByIdDto>(product);

            return new SuccessDataResult<GetProductByIdDto>(productDtos, Messages.ProductIdListed);
        }

        [SecuredOperation("admin")]
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

        [SecuredOperation("admin")]
        public IResult Delete(int productId)
        {
            Product product = _productDal.Get(p => p.Id == productId);

            if (product is null)
            {
                return new ErrorResult(Messages.ProductNotFound);
            }

            product.IsStatus = false;

            _productDal.Update(product);

            return new SuccessResult(Messages.ProductDeleted);
        }

        public IDataResult<List<GetGeneralContentIdDto>> GetProductsByGeneralContentId(int generalContentId)
        {
            var result = _productDal.GetByGeneralContentId(generalContentId);
            if (result is null)
            {
                return new ErrorDataResult<List<GetGeneralContentIdDto>>();
            }
            return new SuccessDataResult<List<GetGeneralContentIdDto>>(result, Messages.ProductIdListed);
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var product = _productDal.GetAll(p => p.Name.ToUpper().Trim() == productName.ToUpper().Trim()).Any();
            if (product)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
