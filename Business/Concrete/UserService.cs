using AutoMapper;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Business.ValidationRules.FluentValidation.User;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using DataAccess.Abstract;
using Entities.DTOs.User;

namespace Business.Concrete
{
    public class UserService : IUserService
    {
        public readonly IMapper _mapper;
        IUserDal _userDal;

        public UserService(IUserDal userDal, IMapper mapper)
        {
            _userDal = userDal;
            _mapper = mapper;
        }

        [SecuredOperation("admin")]
        public IResult Delete(int userId)
        {
            User user = _userDal.Get(u => u.Id == userId);

            if(user is null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            user.IsStatus = false;

            _userDal.Update(user);

            return new SuccessResult(Messages.UserDeleted);
        }

        public IDataResult<List<GetUserDetailDto>> GetAll()
        {
            List<User> user = _userDal.GetAll(u => u.IsStatus);

            List<GetUserDetailDto> userDto = _mapper.Map<List<GetUserDetailDto>>(user);

            return new SuccessDataResult<List<GetUserDetailDto>>(userDto, Messages.UsersListed);
        }

        public IDataResult<GetUserByIdDto> GetById(int userId)
        {
            User user = _userDal.Get(u => u.Id == userId);

            if(user is null)
            {
                return new ErrorDataResult<GetUserByIdDto>(Messages.UserIsNull);
            }

            var userDtos = _mapper.Map<GetUserByIdDto>(user);

            return new SuccessDataResult<GetUserByIdDto>(userDtos, Messages.UserIdListed);
        }

        public IDataResult<User> GetByMail(string email)
        {
            var user = _userDal.Get(u => u.Email == email);

            if (user is null)
            {
                return new ErrorDataResult<User>(Messages.MailNotFound);
            }

            return new SuccessDataResult<User>(user, Messages.UserMailListed);
        }

        [SecuredOperation("admin")]
        public IResult HardDelete(int userId)
        {
            User user = _userDal.Get(u => u.Id == userId);
            if(user is null)
            {
                return new ErrorResult(Messages.NoDataOnThisId);
            }

            _userDal.Delete(user);
            return new SuccessResult(Messages.UserDeleteFromDatabase);
        }

        [ValidationAspect(typeof(UpdateUserValidator))]
        public IResult Update(UpdateUserDto updateUserDto)
        {
            User user = _userDal.Get(u => u.Id == updateUserDto.Id);

            if(user is null)
            {
                return new ErrorResult(Messages.UserNotFound);
            }

            _mapper.Map(updateUserDto, user);

            _userDal.Update(user);

            return new SuccessResult(Messages.UserUpdated);
        }

        [ValidationAspect(typeof(PasswordUpdateValidator))]
        public IResult UpdatePassword(PasswordUpdateDto password)
        {
            var userToCheckResult = GetByMail(password.Email);
            if(userToCheckResult.Data is null)
            {
                return new ErrorResult(Messages.MailNotFound);
            }

            var userToCheck = _mapper.Map<User>(userToCheckResult.Data);

            if (!HashingHelper.VerifyPasswordHash(password.OldPassword, userToCheck.PasswordHash, userToCheck.PasswordSalt))
            {
                return new ErrorResult(Messages.OldPasswordError);
            }

            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePasswordHash(password.NewPassword,out passwordHash,out passwordSalt);

            userToCheck.PasswordHash = passwordHash;
            userToCheck.PasswordSalt = passwordSalt;

            _userDal.Update(userToCheck);

            return new SuccessResult(Messages.PasswordUpdated);
        }
    }
}