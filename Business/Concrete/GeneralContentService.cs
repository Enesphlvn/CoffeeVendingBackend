using AutoMapper;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Business.ValidationRules.FluentValidation.GeneralContent;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs.GeneralContent;

namespace Business.Concrete
{
    public class GeneralContentService : IGeneralContentService
    {
        private readonly IMapper _mapper;
        IGeneralContentDal _generalContentDal;

        public GeneralContentService(IGeneralContentDal generalContentDal, IMapper mapper)
        {
            _generalContentDal = generalContentDal;
            _mapper = mapper;
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(CreateGeneralContentValidator))]
        public IResult Add(CreateGeneralContentDto generalContentDto)
        {
            var generalContent = _mapper.Map<GeneralContent>(generalContentDto);

            IResult result = BusinessRules.Run(CheckIfGeneralContentNameExists(generalContent.Name));

            if (result != null)
            {
                return new ErrorResult(result.Message);
            }

            generalContent.Type = generalContent.Type.ToLowerInvariant();

            _generalContentDal.Add(generalContent);

            return new SuccessResult(Messages.GeneralContentAdded);
        }

        [SecuredOperation("admin")]
        public IResult HardDelete(int generalContentId)
        {
            GeneralContent generalContent = _generalContentDal.Get(g => g.Id == generalContentId);
            if (generalContent is null)
            {
                return new ErrorResult(Messages.NoDataOnThisId);
            }
            _generalContentDal.Delete(generalContent);
            return new SuccessResult(Messages.GeneralContentDeleteFromDatabase);
        }

        public IDataResult<List<GetGeneralContentDto>> GetAll()
        {
            List<GeneralContent> generalContents = _generalContentDal.GetAll(gc => gc.IsStatus);

            List<GetGeneralContentDto> generalContentDtos = _mapper.Map<List<GetGeneralContentDto>>(generalContents);

            return new SuccessDataResult<List<GetGeneralContentDto>>(generalContentDtos, Messages.GeneralContentsListed);
        }

        public IDataResult<GetGeneralContentByIdDto> GetById(int generalContentId)
        {
            var generalContent = _generalContentDal.Get(g => g.Id == generalContentId);

            if (generalContent is null)
            {
                return new ErrorDataResult<GetGeneralContentByIdDto>(Messages.GeneralContentIsNull);
            }

            var generalContentDto = _mapper.Map<GetGeneralContentByIdDto>(generalContent);

            return new SuccessDataResult<GetGeneralContentByIdDto>(generalContentDto, Messages.GeneralContentIdListed);
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(UpdateGeneralContentValidator))]
        public IResult Update(UpdateGeneralContentDto generalContentDto)
        {
            var generalContent = _generalContentDal.Get(g => g.Id == generalContentDto.Id);
            if (generalContent is null)
            {
                return new ErrorResult(Messages.GeneralContentIsNull);
            }

            _mapper.Map(generalContentDto, generalContent);

            CheckCriticalLevel(generalContent);

            _generalContentDal.Update(generalContent);

            return new SuccessResult(Messages.GeneralContentUpdated);
        }

        [SecuredOperation("admin")]
        public IResult Delete(int generalContentId)
        {
            GeneralContent generalContent = _generalContentDal.Get(g => g.Id == generalContentId);

            if (generalContent is null)
            {
                return new ErrorResult(Messages.GeneralContentIsNull);
            }

            generalContent.IsStatus = false;

            _generalContentDal.Update(generalContent);

            return new SuccessResult(Messages.GeneralContentDeleted);
        }

        private IResult CheckIfGeneralContentNameExists(string generalContentName)
        {
            var generalContent = _generalContentDal.GetAll(gc => gc.Name.ToUpper().Trim() == generalContentName.ToUpper().Trim()).Any();

            if (generalContent)
            {
                return new ErrorResult(Messages.GeneralContentNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private void CheckCriticalLevel(GeneralContent generalContent)
        {
            if (generalContent.Value < generalContent.IsCritialLevelValue)
            {
                generalContent.IsCritialLevel = true;
            }
        }
    }
}