using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.Product;

namespace Business.Mapping.Profiles.ProductProfiles
{
    public class CreateProductProfile : Profile
    {
        public CreateProductProfile()
        {
            CreateMap<Product, CreateProductDto>().ReverseMap()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now))
                .ForMember(dest => dest.IsStatus, opt => opt.MapFrom(src => true));
        }
    }
}
