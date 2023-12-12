using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.GeneralContent;

namespace Business.Mapping.Profiles.GeneralContentProfiles
{
    public class GetGeneralContentByIdProfile : Profile
    {
        public GetGeneralContentByIdProfile()
        {
            CreateMap<GeneralContent, GetGeneralContentByIdDto>().ReverseMap();
        }
    }
}
