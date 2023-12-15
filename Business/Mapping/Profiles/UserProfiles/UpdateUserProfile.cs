using AutoMapper;
using Core.Entities.Concrete;
using Entities.DTOs.User;

namespace Business.Mapping.Profiles.UserProfiles
{
    public class UpdateUserProfile : Profile
    {
        public UpdateUserProfile()
        {
            CreateMap<User, UpdateUserDto>().ReverseMap()
                .ForMember(dest => dest.IsStatus, opt => opt.MapFrom(src => true));
        }
    }
}
