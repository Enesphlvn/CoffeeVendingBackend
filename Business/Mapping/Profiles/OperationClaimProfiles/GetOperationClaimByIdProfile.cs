using AutoMapper;
using Core.Entities.Concrete;
using Entities.DTOs.OperationClaim;

namespace Business.Mapping.Profiles.OperationClaimProfiles
{
    public class GetOperationClaimByIdProfile : Profile
    {
        public GetOperationClaimByIdProfile()
        {
            CreateMap<OperationClaim, GetOperationClaimByIdDto>();
        }
    }
}
