using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Jwt;
using Entities.DTOs.User;

namespace Business.Abstract
{
    public interface IAuthService
    {
        IDataResult<User> GetByMail(string email);
        IDataResult<User> Register(RegisterUserDto registerUserDto);
        IDataResult<User> Login(LoginUserDto loginUserDto);
        IDataResult<AccessToken> CreateAccessTokenForLogin(User user);
        IDataResult<List<OperationClaim>> GetClaims(User user);
        IDataResult<AccessToken> CreateAccessTokenForRegister(User user);
    }
}
