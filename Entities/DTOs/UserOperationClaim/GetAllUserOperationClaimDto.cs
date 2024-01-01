using Core.Entities.Abstract;

namespace Entities.DTOs.UserOperationClaim
{
    public class GetAllUserOperationClaimDto :IDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int OperationClaimId { get; set; }
        public string OperationClaimName { get; set; }
    }
}
