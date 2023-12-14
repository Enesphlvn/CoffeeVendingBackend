using AutoMapper;
using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation.GeneralContent;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
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

        [ValidationAspect(typeof(CreateGeneralContentValidator))]
        public IResult Add(CreateGeneralContentDto generalContentDto)
        {
            var generalContent = _mapper.Map<GeneralContent>(generalContentDto);

            IResult result = BusinessRules.Run(CheckIfGeneralContentNameExists(generalContent.Name));

            if (result is null)
            {
                generalContent.Type = generalContent.Type.ToLowerInvariant();

                _generalContentDal.Add(generalContent);

                return new SuccessResult(Messages.GeneralContentAdded);
            }
            return result;
        }

        public IResult HardDelete(int generalContentId)
        {
            GeneralContent generalContent = _generalContentDal.Get(g => g.Id == generalContentId);
            _generalContentDal.Delete(generalContent);
            return new SuccessResult(Messages.GeneralContentDeleteFromDatabase);
        }

        public IDataResult<List<GetGeneralContentDto>> GetAll()
        {
            List<GeneralContent> generalContents = _generalContentDal.GetAll();

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

        [ValidationAspect(typeof(UpdateGeneralContentValidator))]
        public IResult Update(UpdateGeneralContentDto generalContentDto)
        {
            var generalContent = _generalContentDal.Get(g => g.Id == generalContentDto.Id);

            if (generalContent is null)
            {
                return new ErrorResult(Messages.GeneralContentIsNull);
            }

            _mapper.Map(generalContentDto, generalContent);

            _generalContentDal.Update(generalContent);
            return new SuccessResult(Messages.GeneralContentUpdated);
        }

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
            var generalContent = _generalContentDal.GetAll(g => g.Name.ToUpper() == generalContentName.ToUpper()).Any();
            if (generalContent)
            {
                return new ErrorResult(Messages.GeneralContentNameAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
