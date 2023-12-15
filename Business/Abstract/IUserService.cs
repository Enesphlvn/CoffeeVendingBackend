using Core.Entities.Concrete;
using Core.Utilities.Results;
using Entities.DTOs.User;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<GetUserDetailDto>> GetAll();
        IDataResult<GetUserByIdDto> GetById(int userId);
        IDataResult<GetUserByMailDto> GetByMail(string email);
        IResult Update(UpdateUserDto updateUserDto);
        IResult Delete(int userId);
        IResult HardDelete(int userId);
        IResult UpdatePassword(PasswordUpdateDto password);
    }
}
