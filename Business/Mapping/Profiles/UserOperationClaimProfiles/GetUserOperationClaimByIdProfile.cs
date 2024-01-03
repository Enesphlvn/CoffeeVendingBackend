using AutoMapper;
using Core.Entities.Concrete;
using Entities.DTOs.UserOperationClaim;

namespace Business.Mapping.Profiles.UserOperationClaimProfiles
{
    public class GetUserOperationClaimByIdProfile : Profile
    {
        public GetUserOperationClaimByIdProfile()
        {
            CreateMap<UserOperationClaim, GetUserOperationClaimByIdDto>().ReverseMap();
        }
    }
}
