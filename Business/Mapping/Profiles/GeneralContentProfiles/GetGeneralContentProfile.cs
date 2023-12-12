using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.GeneralContent;

namespace Business.Mapping.Profiles.GeneralContentProfiles
{
    public class GetGeneralContentProfile : Profile
    {
        public GetGeneralContentProfile()
        {
            CreateMap<GeneralContent, GetGeneralContentDto>().ReverseMap();
        }
    }
}
