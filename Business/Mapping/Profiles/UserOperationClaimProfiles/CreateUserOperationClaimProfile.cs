using AutoMapper;
using Core.Entities.Concrete;
using Entities.DTOs.UserOperationClaim;

namespace Business.Mapping.Profiles.UserOperationClaimProfiles
{
    public class CreateUserOperationClaimProfile : Profile
    {
        public CreateUserOperationClaimProfile()
        {
            CreateMap<UserOperationClaim, CreateUserOperationClaimDto>().ReverseMap()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTime.UtcNow))
                .ForMember(dest => dest.IsStatus, opt => opt.MapFrom(src => true));
        }
    }
}
