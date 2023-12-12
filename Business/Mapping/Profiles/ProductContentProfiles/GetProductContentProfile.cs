using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.ProductContent;

namespace Business.Mapping.Profiles.ProductContentProfiles
{
    public class GetProductContentProfile : Profile
    {
        public GetProductContentProfile()
        {
            CreateMap<ProductContent, GetProductContentDto>();
        }
    }
}
