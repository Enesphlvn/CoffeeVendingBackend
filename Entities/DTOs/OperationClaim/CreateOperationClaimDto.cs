using Core.Entities.Abstract;

namespace Entities.DTOs.OperationClaim
{
    public class CreateOperationClaimDto : IDto
    {
        public string Name { get; set; }
    }
}
