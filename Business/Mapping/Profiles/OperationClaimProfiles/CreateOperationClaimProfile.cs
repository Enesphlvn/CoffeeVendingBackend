using AutoMapper;
using Core.Entities.Concrete;
using Entities.DTOs.OperationClaim;

namespace Business.Mapping.Profiles.OperationClaimProfiles
{
    public class CreateOperationClaimProfile : Profile
    {
        public CreateOperationClaimProfile()
        {
            CreateMap<OperationClaim, CreateOperationClaimDto>().ReverseMap()
                .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => DateTimeOffset.Now))
                .ForMember(dest => dest.IsStatus, opt => opt.MapFrom(src => true));
        }
    }
}
