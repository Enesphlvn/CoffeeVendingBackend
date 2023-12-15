using AutoMapper;
using Business.Abstract;
using Business.Constans;
using Business.ValidationRules.FluentValidation.OperationClaim;
using Business.ValidationRules.FluentValidation.UserOperationClaim;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.DTOs.UserOperationClaim;

namespace Business.Concrete
{
    public class UserOperationClaimService : IUserOperationClaimService
    {
        private readonly IMapper _mapper;
        IUserOperationClaimDal _userOperationClaimDal;
        IUserDal _userDal;
        IOperationClaimDal _operationClaimDal;

        public UserOperationClaimService(IUserOperationClaimDal userOperationClaimDal, IMapper mapper, IUserDal userDal, IOperationClaimDal operationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
            _operationClaimDal = operationClaimDal;
            _mapper = mapper;
            _userDal = userDal;
        }

        [ValidationAspect(typeof(CreateUserOperationClaimValidator))]
        public IResult Add(CreateUserOperationClaimDto userOperationClaimDto)
        {
            var userExists = _userDal.Get(u => u.Id == userOperationClaimDto.UserId);
            if (userExists is null)
            {
                return new ErrorResult(Messages.UserIsNull);
            }

            var operationClaimExists = _operationClaimDal.Get(o => o.Id == userOperationClaimDto.OperationClaimId);
            if (operationClaimExists is null)
            {
                return new ErrorResult(Messages.OperationClaimIsNull);
            }

            var userOperationClaim = _mapper.Map<UserOperationClaim>(userOperationClaimDto);

            _userOperationClaimDal.Add(userOperationClaim);
            return new SuccessResult(Messages.UserOperationClaimAdded);
        }

        public IResult Delete(int userOperationClaimId)
        {
            UserOperationClaim userOperationClaim = _userOperationClaimDal.Get(u => u.Id == userOperationClaimId);

            if (userOperationClaim is null)
            {
                return new ErrorResult(Messages.UserOperationClaimIsNull);
            }

            userOperationClaim.IsStatus = false;

            _userOperationClaimDal.Update(userOperationClaim);
            return new SuccessResult(Messages.UserOperationClaimDeleted);
        }

        public IDataResult<List<GetUserOperationClaimDto>> GetAll()
        {
            List<UserOperationClaim> userOperationClaims = _userOperationClaimDal.GetAll();

            List<GetUserOperationClaimDto> userOperationClaimDtos = _mapper.Map<List<GetUserOperationClaimDto>>(userOperationClaims);

            return new SuccessDataResult<List<GetUserOperationClaimDto>>(userOperationClaimDtos, Messages.UserOperationClaimsListed);
        }

        public IDataResult<List<UserOperationClaimDetailDto>> GetUserOperationClaimDetails()
        {
            return new SuccessDataResult<List<UserOperationClaimDetailDto>>(_userOperationClaimDal.GetUserOperationClaimDetails(), Messages.UserOperationClaimsListed);
        }

        public IResult HardDelete(int userOperationClaimId)
        {
            var userOperationClaim = _userOperationClaimDal.Get(u => u.Id == userOperationClaimId);
            if (userOperationClaim is null)
            {
                return new ErrorResult(Messages.NoDataOnThisId);
            }
            _userOperationClaimDal.Delete(userOperationClaim);
            return new SuccessResult(Messages.UserOperationClaimDeleteFromDatabase);
        }

        [ValidationAspect(typeof(UpdateUserOperationClaimValidator))]
        public IResult Update(UpdateUserOperationClaimDto userOperationClaimDto)
        {
            var userOperationClaim = _userOperationClaimDal.Get(u => u.Id == userOperationClaimDto.Id);
            if (userOperationClaim is null)
            {
                return new ErrorResult(Messages.UserOperationClaimIsNull);
            }

            var userExists = _userDal.Get(u => u.Id == userOperationClaimDto.UserId);
            if (userExists is null)
            {
                return new ErrorResult(Messages.UserIsNull);
            }

            var operationClaimExists = _operationClaimDal.Get(o => o.Id == userOperationClaimDto.OperationClaimId);
            if (operationClaimExists is null)
            {
                return new ErrorResult(Messages.OperationClaimIsNull);
            }

            _mapper.Map(userOperationClaimDto, userOperationClaim);

            _userOperationClaimDal.Update(userOperationClaim);
            return new SuccessResult(Messages.UserOperationClaimUpdated);
        }
    }
}
