using Core.Entities.Abstract;

namespace Entities.DTOs.OperationClaim
{
    public class GetOperationClaimByIdDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
