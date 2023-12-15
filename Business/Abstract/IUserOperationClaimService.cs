using Core.Utilities.Results;
using Entities.DTOs.UserOperationClaim;

namespace Business.Abstract
{
    public interface IUserOperationClaimService
    {
        IDataResult<List<GetUserOperationClaimDto>> GetAll();
        IDataResult<List<UserOperationClaimDetailDto>> GetUserOperationClaimDetails();
        IResult Add(CreateUserOperationClaimDto userOperationClaimDto);
        IResult Update(UpdateUserOperationClaimDto userOperationClaimDto);
        IResult Delete(int userOperationClaimId);
        IResult HardDelete(int userOperationClaimId);
    }
}
