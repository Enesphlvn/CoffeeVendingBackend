using AutoMapper;
using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation.User;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.Jwt;
using DataAccess.Abstract;
using Entities.DTOs.User;

namespace Business.Concrete
{
    public class AuthService : IAuthService
    {
        public readonly IMapper _mapper;
        ITokenHelper _tokenHelper;
        IUserDal _userDal;

        public AuthService(IUserDal userDal, ITokenHelper tokenHelper, IMapper mapper)
        {
            _userDal = userDal;
            _tokenHelper = tokenHelper;
            _mapper = mapper;
        }

        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        [ValidationAspect(typeof(LoginUserValidator))]
        public IDataResult<User> Login(LoginUserDto loginUserDto)
        {
            var userToCheckResult = GetByMail(loginUserDto.Email);
            if (userToCheckResult.Data is null)
            {
                return new ErrorDataResult<User>(Messages.MailNotFound);
            }

            var userToCheck = userToCheckResult.Data;

            if (!HashingHelper.VerifyPasswordHash(loginUserDto.Password, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorDataResult<User>(Messages.PasswordError);
            }

            return new SuccessDataResult<User>(userToCheck, Messages.SuccessfulLogin);
        }

        [ValidationAspect(typeof(RegisterUserValidator))]
        public IDataResult<User> Register(RegisterUserDto registerUserDto)
        {
            var result = UserMailExists(registerUserDto.Email);
            if (!result.Success)
            {
                return new ErrorDataResult<User>(result.Message);
            }
            var user = CreateUserWithHashedPassword(registerUserDto);

            _userDal.Add(user);

            return new SuccessDataResult<User>(user, Messages.UserRegistered);
        }

        public IDataResult<AccessToken> CreateAccessToken(User user)
        {
            var claims = GetClaims(user);
            var accessToken = _tokenHelper.CreateToken(user, claims.Data);
            return new SuccessDataResult<AccessToken>(accessToken);
        }

        public IDataResult<User> GetByMail(string email)
        {
            var user = _userDal.Get(u => u.Email == email);
            if (user is null)
            {
                return new ErrorDataResult<User>(Messages.MailNotFound);
            }

            return new SuccessDataResult<User>(user);
        }

        private User CreateUserWithHashedPassword(RegisterUserDto userForRegisterDto)
        {
            byte[] passwordhash, passwordSalt;
            HashingHelper.CreatePasswordHash(userForRegisterDto.Password, out passwordhash, out passwordSalt);
            return new User
            {
                Email = userForRegisterDto.Email,
                FirstName = userForRegisterDto.FirstName,
                LastName = userForRegisterDto.LastName,
                PasswordHash = passwordhash,
                PasswordSalt = passwordSalt,
                CreatedAt = DateTimeOffset.Now, 
                IsStatus = true
            };
        }

        private IResult UserMailExists(string email)
        {
            var user = GetByMail(email);

            if (user.Data != null)
            {
                return new ErrorResult(Messages.UserAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
