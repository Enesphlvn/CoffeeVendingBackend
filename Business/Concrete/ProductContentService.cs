using AutoMapper;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Business.ValidationRules.FluentValidation.ProductContent;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs.ProductContent;

namespace Business.Concrete
{
    public class ProductContentService : IProductContentService
    {
        private readonly IMapper _mapper;
        IProductContentDal _productContentDal;
        IGeneralContentDal _GeneralContentDal;
        IProductDal _productDal;

        public ProductContentService(IProductContentDal productContentDal, IMapper mapper, IGeneralContentDal GeneralContentDal, IProductDal productDal)
        {
            _productContentDal = productContentDal;
            _GeneralContentDal = GeneralContentDal;
            _productDal = productDal;
            _mapper = mapper;
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(CreateProductContentValidator))]
        public IResult Add(CreateProductContentDto productContentDto)
        {
            var productContent = _mapper.Map<ProductContent>(productContentDto);

            IResult generalContentExists = BusinessRules.Run(CheckIfGeneralContentIdExists(productContent.GeneralContentId), CheckIfProductIdExists(productContent.ProductId));
            if(generalContentExists != null)
            {
                return new ErrorResult(generalContentExists.Message);
            }

            _productContentDal.Add(productContent);
            return new SuccessResult(Messages.ProductContentAdded);
        }

        [SecuredOperation("admin")]
        public IResult HardDelete(int productContentId)
        {
            ProductContent productContent = _productContentDal.Get(p => p.Id == productContentId);
            if(productContent is null)
            {
                return new ErrorResult(Messages.NoDataOnThisId);
            }
            _productContentDal.Delete(productContent);
            return new SuccessResult(Messages.ProductContentDeleteFromDatabase);
        }

        public IDataResult<List<GetAllProductContentDto>> GetAll()
        {
            return new SuccessDataResult<List<GetAllProductContentDto>>(_productContentDal.GetProductContentDetails(), Messages.ProductContentsListed);
        }

        public IDataResult<GetProductContentByIdDto> GetById(int productContentId)
        {
            var productContent = _productContentDal.Get(p => p.Id == productContentId);

            if (productContent is null)
            {
                return new ErrorDataResult<GetProductContentByIdDto>(Messages.ProductContentIsNull);
            }

            var productContentDto = _mapper.Map<GetProductContentByIdDto>(productContent);

            return new SuccessDataResult<GetProductContentByIdDto>(productContentDto, Messages.ProductContentIdListed);
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(UpdateProductContentValidator))]
        public IResult Update(UpdateProductContentDto productContentDto)
        {
            var productContent = _productContentDal.Get(p => p.Id == productContentDto.Id);

            if (productContent is null)
            {
                return new ErrorResult(Messages.ProductContentIsNull);
            }

            _mapper.Map(productContentDto, productContent);

            IResult result = BusinessRules.Run(CheckIfGeneralContentIdExists(productContent.GeneralContentId), CheckIfProductIdExists(productContent.ProductId));
            if(result != null)
            {
                return new ErrorResult(result.Message);
            }

            _productContentDal.Update(productContent);
            return new SuccessResult(Messages.ProductContentUpdated);
        }

        [SecuredOperation("admin")]
        public IResult Delete(int productContentId)
        {
            ProductContent productContent = _productContentDal.Get(p => p.Id == productContentId);

            if (productContent is null)
            {
                return new ErrorResult(Messages.ProductContentIsNull);
            }

            productContent.IsStatus = false;

            _productContentDal.Update(productContent);

            return new SuccessResult(Messages.ProductContentDeleted);
        }

        private IResult CheckIfGeneralContentIdExists(int generalContentId)
        {
            var generalContent = _GeneralContentDal.Get(g => g.Id == generalContentId);
            if(generalContent is null || generalContent.IsStatus == false)
            {
                return new ErrorResult(Messages.GeneralContentIsNull);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductIdExists(int productId)
        {
            var product = _productDal.Get(p => p.Id == productId);
            if(product is null || product.IsStatus == false)
            {
                return new ErrorResult(Messages.ProductIsNull);
            }
            return new SuccessResult();
        }
    }
}
