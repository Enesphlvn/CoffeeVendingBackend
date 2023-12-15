using AutoMapper;
using Core.Entities.Concrete;
using Entities.DTOs.UserOperationClaim;

namespace Business.Mapping.Profiles.UserOperationClaimProfiles
{
    public class GetUserOperationClaimProfile : Profile
    {
        public GetUserOperationClaimProfile()
        {
            CreateMap<UserOperationClaim, GetUserOperationClaimDto>().ReverseMap();
        }
    }
}
