using Core.DataAccess;
using Core.Entities.Concrete;
using Entities.DTOs.UserOperationClaim;

namespace DataAccess.Abstract
{
    public interface IUserOperationClaimDal : IEntityRepository<UserOperationClaim>
    {
        List<UserOperationClaimDetailDto> GetUserOperationClaimDetails();
    }
}
