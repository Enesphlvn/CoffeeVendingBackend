using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.ProductContent;

namespace Business.Mapping.Profiles.ProductContentProfiles
{
    public class GetProductContentByIdProfile : Profile
    {
        public GetProductContentByIdProfile()
        {
            CreateMap<ProductContent, GetProductContentByIdDto>();
        }
    }
}
