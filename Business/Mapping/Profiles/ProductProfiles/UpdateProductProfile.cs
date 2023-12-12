using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.Product;

namespace Business.Mapping.Profiles.ProductProfiles
{
    public class UpdateProductProfile : Profile
    {
        public UpdateProductProfile()
        {
            CreateMap<Product, UpdateProductDto>().ReverseMap()
                .ForMember(dest => dest.IsStatus, opt => opt.MapFrom(src => true));
        }
    }
}