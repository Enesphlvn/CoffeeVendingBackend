using AutoMapper;
using Core.Entities.Concrete;
using Entities.DTOs.OperationClaim;

namespace Business.Mapping.Profiles.OperationClaimProfiles
{
    public class OperationClaimDetailProfile : Profile
    {
        public OperationClaimDetailProfile()
        {
            CreateMap<OperationClaim, OperationClaimDetailDto>().ReverseMap();
        }
    }
}
