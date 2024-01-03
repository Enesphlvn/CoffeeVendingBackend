using AutoMapper;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constans;
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
        IOperationClaimDal _operationClaimDal;
        IUserDal _userDal;

        public UserOperationClaimService(IUserOperationClaimDal userOperationClaimDal, IMapper mapper, IUserDal userDal, IOperationClaimDal operationClaimDal)
        {
            _userOperationClaimDal = userOperationClaimDal;
            _operationClaimDal = operationClaimDal;
            _mapper = mapper;
            _userDal = userDal;
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(CreateUserOperationClaimValidator))]
        public IResult Add(CreateUserOperationClaimDto userOperationClaimDto)
        {
            var userOperationClaim = _mapper.Map<UserOperationClaim>(userOperationClaimDto);

            IResult result = BusinessRules.Run(CheckIfOperationClaimIdExists(userOperationClaim.OperationClaimId), CheckIfUserIdExists(userOperationClaim.UserId));
            if(result != null)
            {
                return new ErrorResult(result.Message);
            }

            _userOperationClaimDal.Add(userOperationClaim);
            return new SuccessResult(Messages.UserOperationClaimAdded);
        }

        [SecuredOperation("admin")]
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

        public IDataResult<List<GetAllUserOperationClaimDto>> GetAll()
        {
            return new SuccessDataResult<List<GetAllUserOperationClaimDto>>(_userOperationClaimDal.GetUserOperationClaimDetails(), Messages.UserOperationClaimsListed);
        }

        public IDataResult<GetUserOperationClaimByIdDto> GetById(int userOperationClaimId)
        {
            var userOperationClaim = _userOperationClaimDal.Get(uoc => uoc.Id == userOperationClaimId);

            if (userOperationClaim is null)
            {
                return new ErrorDataResult<GetUserOperationClaimByIdDto>(Messages.UserOperationClaimIsNull);
            }

            var userOperationClaimDto = _mapper.Map<GetUserOperationClaimByIdDto>(userOperationClaim);

            return new SuccessDataResult<GetUserOperationClaimByIdDto>(userOperationClaimDto, Messages.UserOperationClaimIdListed);
        }

        [SecuredOperation("admin")]
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

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(UpdateUserOperationClaimValidator))]
        public IResult Update(UpdateUserOperationClaimDto userOperationClaimDto)
        {
            var userOperationClaim = _userOperationClaimDal.Get(u => u.Id == userOperationClaimDto.Id);
            if (userOperationClaim is null)
            {
                return new ErrorResult(Messages.UserOperationClaimIsNull);
            }

            _mapper.Map(userOperationClaimDto, userOperationClaim);

            IResult result = BusinessRules.Run(CheckIfOperationClaimIdExists(userOperationClaim.OperationClaimId), CheckIfUserIdExists(userOperationClaim.UserId));
            if(result != null)
            {
                return new ErrorResult(result.Message);
            }

            _userOperationClaimDal.Update(userOperationClaim);
            return new SuccessResult(Messages.UserOperationClaimUpdated);
        }

        private IResult CheckIfUserIdExists(int userId)
        {
            var user = _userDal.Get(u => u.Id == userId);
            if (user is null || user.IsStatus == false)
            {
                return new ErrorResult(Messages.UserIsNull);
            }
            return new SuccessResult();
        }

        private IResult CheckIfOperationClaimIdExists(int operationClaimId)
        {
            var operationClaim = _operationClaimDal.Get(o => o.Id == operationClaimId);
            if (operationClaim is null || operationClaim.IsStatus == false)
            {
                return new ErrorResult(Messages.OperationClaimIsNull);
            }
            return new SuccessResult();
        }
    }
}
