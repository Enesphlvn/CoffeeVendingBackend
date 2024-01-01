using Core.Utilities.Results;
using Entities.DTOs.UserOperationClaim;

namespace Business.Abstract
{
    public interface IUserOperationClaimService
    {
        IDataResult<List<GetAllUserOperationClaimDto>> GetAll();
        IResult Add(CreateUserOperationClaimDto userOperationClaimDto);
        IResult Update(UpdateUserOperationClaimDto userOperationClaimDto);
        IResult Delete(int userOperationClaimId);
        IResult HardDelete(int userOperationClaimId);
    }
}
