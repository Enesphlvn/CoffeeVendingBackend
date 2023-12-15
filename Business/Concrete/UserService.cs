﻿using AutoMapper;
using Business.Abstract;
using Business.Constans;
using Core.Entities.Concrete;
using Core.Utilities.Results;
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
            List<User> user = _userDal.GetAll();

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

        public IDataResult<GetUserByMailDto> GetByMail(string email)
        {
            var user = _userDal.Get(u => u.Email == email);

            if (user is null)
            {
                return new ErrorDataResult<GetUserByMailDto>(Messages.MailNotFound);
            }

            var userDto = _mapper.Map<GetUserByMailDto>(user);

            return new SuccessDataResult<GetUserByMailDto>(userDto);
        }

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

        public IResult UpdatePassword(User user)
        {
            throw new NotImplementedException();
        }
    }
}