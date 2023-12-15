using Core.Entities.Abstract;

namespace Entities.DTOs.UserOperationClaim
{
    public class UpdateUserOperationClaimDto : IDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int OperationClaimId { get; set; }
    }
}
