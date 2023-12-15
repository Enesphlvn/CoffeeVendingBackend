using AutoMapper;
using Core.Entities.Concrete;
using Entities.DTOs.UserOperationClaim;

namespace Business.Mapping.Profiles.UserOperationClaimProfiles
{
    public class UpdateUserOperationClaimProfile : Profile
    {
        public UpdateUserOperationClaimProfile()
        {
            CreateMap<UserOperationClaim, UpdateUserOperationClaimDto>().ReverseMap()
                .ForMember(dest => dest.IsStatus, opt => opt.MapFrom(src => true));
        }
    }
}
