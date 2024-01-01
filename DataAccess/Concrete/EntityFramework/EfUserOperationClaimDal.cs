using Core.DataAccess;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.DTOs.UserOperationClaim;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserOperationClaimDal : EfEntityRepositoryBase<UserOperationClaim, CoffeeVendingContext>, IUserOperationClaimDal
    {
        public List<GetAllUserOperationClaimDto> GetUserOperationClaimDetails()
        {
            using (CoffeeVendingContext context = new CoffeeVendingContext())
            {
                var result = from uoc in context.UserOperationClaims
                             join u in context.Users
                             on uoc.UserId equals u.Id
                             join oc in context.OperationClaims
                             on uoc.OperationClaimId equals oc.Id
                             select new GetAllUserOperationClaimDto
                             {
                                 Id = uoc.Id,
                                 UserId = u.Id,
                                 UserName = (u.FirstName + " " + u.LastName),
                                 OperationClaimId = oc.Id,
                                 OperationClaimName = oc.Name
                             };
                return result.ToList();
            }
        }
    }
}
