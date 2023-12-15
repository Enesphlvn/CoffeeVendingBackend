using AutoMapper;
using Core.Entities.Concrete;
using Entities.DTOs.OperationClaim;

namespace Business.Mapping.Profiles.OperationClaimProfiles
{
    public class UpdateOperationClaimProfile : Profile
    {
        public UpdateOperationClaimProfile()
        {
            CreateMap<OperationClaim, UpdateOperationClaimDto>().ReverseMap()
                .ForMember(dest => dest.IsStatus, opt => opt.MapFrom(src => true));
        }
    }
}
