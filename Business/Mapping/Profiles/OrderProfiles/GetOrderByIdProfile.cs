using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.Order;

namespace Business.Mapping.Profiles.OrderProfiles
{
    public class GetOrderByIdProfile : Profile
    {
        public GetOrderByIdProfile()
        {
            CreateMap<Order, GetOrderByIdDto>().ReverseMap();
        }
    }
}
