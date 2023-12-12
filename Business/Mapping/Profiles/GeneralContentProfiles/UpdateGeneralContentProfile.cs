using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.GeneralContent;

namespace Business.Mapping.Profiles.GeneralContentProfiles
{
    public class UpdateGeneralContentProfile : Profile
    {
        public UpdateGeneralContentProfile()
        {
            CreateMap<GeneralContent, UpdateGeneralContentDto>().ReverseMap()
                .ForMember(dest => dest.IsStatus, opt => opt.MapFrom(src => true));
        }
    }
}
