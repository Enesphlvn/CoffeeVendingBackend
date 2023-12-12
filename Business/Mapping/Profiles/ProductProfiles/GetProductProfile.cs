using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.Product;

namespace Business.Mapping.Profiles.ProductProfiles
{
    public class GetProductProfile : Profile
    {
        public GetProductProfile()
        {
            CreateMap<Product, GetProductDto>().ReverseMap();
        }
    }
}
