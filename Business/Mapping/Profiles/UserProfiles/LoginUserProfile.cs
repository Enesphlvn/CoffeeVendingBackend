using AutoMapper;
using Core.Entities.Concrete;
using Entities.DTOs.User;

namespace Business.Mapping.Profiles.UserProfiles
{
    public class LoginUserProfile : Profile
    {
        public LoginUserProfile()
        {
            CreateMap<User, LoginUserDto>().ReverseMap();
        }
    }
}
