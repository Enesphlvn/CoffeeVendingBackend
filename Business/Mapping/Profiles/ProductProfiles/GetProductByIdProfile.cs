using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.Product;

namespace Business.Mapping.Profiles.ProductProfiles
{
    public class GetProductByIdProfile : Profile
    {
        public GetProductByIdProfile()
        {
            CreateMap<Product, GetProductByIdDto>().ReverseMap();
        }
    }
}