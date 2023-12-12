using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.ProductContent;

namespace Business.Mapping.Profiles.ProductContentProfiles
{
    public class UpdateProductContentProfile : Profile
    {
        public UpdateProductContentProfile()
        {
            CreateMap<ProductContent, UpdateProductContentDto>().ReverseMap()
                .ForMember(dest => dest.IsStatus, opt => opt.MapFrom(src => true));
        }
    }
}
