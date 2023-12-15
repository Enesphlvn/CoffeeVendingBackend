using Core.DataAccess;
using Core.Entities.Concrete;
using DataAccess.Abstract;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfOperationClaimDal : EfEntityRepositoryBase<OperationClaim, CoffeeVendingContext>, IOperationClaimDal
    {
    }
}
