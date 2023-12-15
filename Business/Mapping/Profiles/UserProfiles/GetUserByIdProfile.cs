using AutoMapper;
using Core.Entities.Concrete;
using Entities.DTOs.User;

namespace Business.Mapping.Profiles.UserProfiles
{
    public class GetUserByIdProfile : Profile
    {
        public GetUserByIdProfile()
        {
            CreateMap<User, GetUserByIdDto>().ReverseMap();
        }
    }
}
