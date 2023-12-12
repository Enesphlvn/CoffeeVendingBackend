using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.GeneralContent;

namespace Business.Mapping.Profiles.GeneralContentProfiles
{
    public class CreateGeneralContentProfile : Profile
    {
        public CreateGeneralContentProfile()
        {
            CreateMap<GeneralContent, CreateGeneralContentDto>().ReverseMap()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsStatus, opt => opt.MapFrom(src => true))
                .ForMember(dest => dest.IsCritialLevel, opt => opt.MapFrom(src => false));
        }
    }
}
