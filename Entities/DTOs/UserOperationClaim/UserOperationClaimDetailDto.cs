using Core.Entities.Abstract;

namespace Entities.DTOs.UserOperationClaim
{
    public class UserOperationClaimDetailDto :IDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string OperationClaimName { get; set; }
    }
}
