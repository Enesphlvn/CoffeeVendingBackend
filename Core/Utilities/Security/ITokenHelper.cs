using Core.Entities.Concrete;
using Core.Utilities.Security.Jwt;

namespace Core.Utilities.Security
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
    }
}