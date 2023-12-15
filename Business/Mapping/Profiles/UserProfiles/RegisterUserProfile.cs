using AutoMapper;
using Core.Entities.Concrete;
using Entities.DTOs.User;

namespace Business.Mapping.Profiles.UserProfiles
{
    public class RegisterUserProfile : Profile
    {
        public RegisterUserProfile()
        {
            CreateMap<User, RegisterUserDto>().ReverseMap()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsStatus, opt => opt.MapFrom(src => true));
        }
    }
}
