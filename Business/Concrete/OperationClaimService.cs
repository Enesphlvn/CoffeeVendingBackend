﻿using AutoMapper;
using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constans;
using Business.ValidationRules.FluentValidation.OperationClaim;
using Core.Aspects.Autofac.Validation;
using Core.Entities.Concrete;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.DTOs.OperationClaim;

namespace Business.Concrete
{
    public class OperationClaimService : IOperationClaimService
    {
        private readonly IMapper _mapper;
        IOperationClaimDal _operationClaimDal;

        public OperationClaimService(IOperationClaimDal operationClaimDal, IMapper mapper)
        {
            _operationClaimDal = operationClaimDal;
            _mapper = mapper;
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(CreateOperationClaimValidator))]
        public IResult Add(CreateOperationClaimDto operationClaimDto)
        {
            var operationClaim = _mapper.Map<OperationClaim>(operationClaimDto);

            IResult result = BusinessRules.Run(CheckIfOperationClaimNameExists(operationClaim.Name));

            if (result != null)
            {
                return new ErrorResult(result.Message);
            }

            _operationClaimDal.Add(operationClaim);
            return new SuccessResult(Messages.OperationClaimAdded);
        }

        [SecuredOperation("admin")]
        public IResult Delete(int operationClaimId)
        {
            var operationClaim = _operationClaimDal.Get(o => o.Id == operationClaimId);
            if(operationClaim is null)
            {
                return new ErrorResult(Messages.OperationClaimIsNull);
            }

            operationClaim.IsStatus = false;

            _operationClaimDal.Update(operationClaim);
            return new SuccessResult(Messages.OperationClaimDeleted);
        }

        public IDataResult<List<OperationClaimDetailDto>> GetAll()
        {
            List<OperationClaim> operationClaims = _operationClaimDal.GetAll(oc => oc.IsStatus);

            List<OperationClaimDetailDto> operationClaimDtos = _mapper.Map<List<OperationClaimDetailDto>>(operationClaims);
            return new SuccessDataResult<List<OperationClaimDetailDto>>(operationClaimDtos, Messages.OperationClaimsListed);
        }

        public IDataResult<GetOperationClaimByIdDto> GetById(int operationClaimId)
        {
            var operationClaim = _operationClaimDal.Get(oc => oc.Id == operationClaimId);

            if( operationClaim is null)
            {
                return new ErrorDataResult<GetOperationClaimByIdDto>(Messages.OperationClaimIsNull);
            }

            var operationClaimDto = _mapper.Map<GetOperationClaimByIdDto>(operationClaim);

            return new SuccessDataResult<GetOperationClaimByIdDto>(operationClaimDto, Messages.OperationClaimIdListed);
        }

        [SecuredOperation("admin")]
        public IResult HardDelete(int operationClaimId)
        {
            OperationClaim operationClaim = _operationClaimDal.Get(o => o.Id == operationClaimId);
            if(operationClaim is null)
            {
                return new ErrorResult(Messages.NoDataOnThisId);
            }
            _operationClaimDal.Delete(operationClaim);
            return new SuccessResult(Messages.OperationClaimDeleteFromDatabase);
        }

        [SecuredOperation("admin")]
        [ValidationAspect(typeof(UpdateOperationClaimValidator))]
        public IResult Update(UpdateOperationClaimDto operationClaimDto)
        {
            var operationClaim = _operationClaimDal.Get(o => o.Id == operationClaimDto.Id);
            if(operationClaim is null)
            {
                return new ErrorResult(Messages.OperationClaimIsNull);
            }

            _mapper.Map(operationClaimDto, operationClaim);

            _operationClaimDal.Update(operationClaim);
            return new SuccessResult(Messages.OperationClaimUpdated);
        }
            
        private IResult CheckIfOperationClaimNameExists(string operationClaimName)
        {
            var operationClaim = _operationClaimDal.GetAll(o => o.Name.Trim().ToUpper() == operationClaimName.Trim().ToUpper()).Any();

            if (operationClaim)
            {
                return new ErrorResult(Messages.OperationClaimAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}
