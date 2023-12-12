using Core.Utilities.Results;
using Entities.DTOs.GeneralContent;

namespace Business.Abstract
{
    public interface IGeneralContentService
    {
        IDataResult<List<GetGeneralContentDto>> GetAll();
        IResult Add(CreateGeneralContentDto generalContentDto);
        IResult Update(UpdateGeneralContentDto generalContentDto);
        IResult Delete(int generalContentId);
        IResult HardDelete(int generalContentId);
        IDataResult<GetGeneralContentByIdDto> GetById(int generalContentId);
    }
}
