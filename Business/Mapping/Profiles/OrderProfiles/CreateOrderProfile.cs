using AutoMapper;
using Entities.Concrete;
using Entities.DTOs.Order;

namespace Business.Mapping.Profiles.OrderProfiles
{
    public class CreateOrderProfile : Profile
    {
        public CreateOrderProfile()
        {
            CreateMap<Order, CreateOrderDto>().ReverseMap()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsStatus, opt => opt.MapFrom(src => true));
        }
    }
}
