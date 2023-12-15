using Core.Entities.Abstract;

namespace Entities.DTOs.UserOperationClaim
{
    public class CreateUserOperationClaimDto : IDto
    {
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
    }
}
