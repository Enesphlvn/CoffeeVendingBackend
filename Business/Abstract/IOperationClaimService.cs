using Core.Utilities.Results;
using Entities.DTOs.OperationClaim;

namespace Business.Abstract
{
    public interface IOperationClaimService
    {
        IDataResult<List<OperationClaimDetailDto>> GetAll();
        IResult Add(CreateOperationClaimDto operationClaimDto);
        IResult Update(UpdateOperationClaimDto operationClaimDto);
        IResult Delete(int operationClaimId);
        IResult HardDelete(int operationClaimId);
    }
}