using AutoMapper;
using Core.Entities.Concrete;
using Entities.DTOs.User;

namespace Business.Mapping.Profiles.UserProfiles
{
    public class GetUserByMailProfile : Profile
    {
        public GetUserByMailProfile()
        {
            CreateMap<User, GetUserByMailDto>().ReverseMap();
        }
    }
}