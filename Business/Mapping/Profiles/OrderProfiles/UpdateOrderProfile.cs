using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.Order;

namespace Business.Mapping.Profiles.OrderProfiles
{
    public class UpdateOrderProfile : Profile
    {
        public UpdateOrderProfile()
        {
            CreateMap<Order, UpdateOrderDto>().ReverseMap()
                .ForMember(dest => dest.IsStatus, opt => opt.MapFrom(src => true));
        }
    }
}
