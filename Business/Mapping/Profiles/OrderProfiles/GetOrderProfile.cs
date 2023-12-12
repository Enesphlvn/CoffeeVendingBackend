using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.Order;

namespace Business.Mapping.Profiles.OrderProfiles
{
    public class GetOrderProfile : Profile
    {
        public GetOrderProfile()
        {
            CreateMap<Order, GetOrderDto>().ReverseMap();
        }
    }
}
