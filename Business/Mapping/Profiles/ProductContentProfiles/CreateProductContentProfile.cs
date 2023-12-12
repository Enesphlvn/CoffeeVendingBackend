using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.ProductContent;

namespace Business.Mapping.Profiles.ProductContentProfiles
{
    public class CreateProductContentProfile : Profile
    {
        public CreateProductContentProfile()
        {
            CreateMap<ProductContent, CreateProductContentDto>().ReverseMap()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsStatus, opt => opt.MapFrom(src => true));
        }
    }
}
