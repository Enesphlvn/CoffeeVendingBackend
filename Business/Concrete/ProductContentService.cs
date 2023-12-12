using AutoMapper;
using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation.ProductContent;
using Core.Aspects.Autofac.Validation;
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

        public ProductContentService(IProductContentDal productContentDal, IMapper mapper)
        {
            _productContentDal = productContentDal;
            _mapper = mapper;
        }

        [ValidationAspect(typeof(CreateProductContentValidator))]
        public IResult Add(CreateProductContentDto productContentDto)
        {
            var productContent = _mapper.Map<ProductContent>(productContentDto);

            _productContentDal.Add(productContent);
            return new SuccessResult(Messages.ProductContentAdded);
        }

        public IResult HardDelete(int productContentId)
        {
            ProductContent productContent = _productContentDal.Get(p => p.Id == productContentId);
            _productContentDal.Delete(productContent);
            return new SuccessResult(Messages.ProductContentDeleteFromDatabase);
        }

        public IDataResult<List<GetProductContentDto>> GetAll()
        {
            List<ProductContent> productContents = _productContentDal.GetAll();

            List<GetProductContentDto> productContentDtos = _mapper.Map<List<GetProductContentDto>>(productContents);

            return new SuccessDataResult<List<GetProductContentDto>>(productContentDtos, Messages.ProductContentsListed);
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

        [ValidationAspect(typeof(UpdateProductContentValidator))]
        public IResult Update(UpdateProductContentDto productContentDto)
        {
            var productContent = _productContentDal.Get(p => p.Id == productContentDto.Id);

            if (productContent is null)
            {
                return new ErrorResult(Messages.ProductContentIsNull);
            }

            _mapper.Map(productContentDto, productContent);

            _productContentDal.Update(productContent);
            return new SuccessResult(Messages.ProductContentUpdated);
        }

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

        public IDataResult<List<GetProductContentDetailDto>> GetProductContentDetails()
        {
            return new SuccessDataResult<List<GetProductContentDetailDto>>(_productContentDal.GetProductContentDetails(), Messages.ProductContentsListed);
        }
    }
}
