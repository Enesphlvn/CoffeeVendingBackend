using AutoMapper;
using Core.Entities.Concrete;
using Entities.DTOs.User;

namespace Business.Mapping.Profiles.UserProfiles
{
    public class GetUserProfile : Profile
    {
        public GetUserProfile()
        {
            CreateMap<User, GetUserDetailDto>().ReverseMap();
        }
    }
}
