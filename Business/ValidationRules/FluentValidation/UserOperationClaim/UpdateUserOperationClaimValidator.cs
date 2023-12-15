using Entities.DTOs.UserOperationClaim;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.UserOperationClaim
{
    public class UpdateUserOperationClaimValidator : AbstractValidator<UpdateUserOperationClaimDto>
    {
        public UpdateUserOperationClaimValidator()
        {
            RuleFor(o => o.Id).NotEmpty()
                .GreaterThan(0);

            RuleFor(o => o.UserId).NotEmpty()
                .GreaterThan(0);

            RuleFor(o => o.OperationClaimId).NotEmpty()
                .GreaterThan(0);
        }
    }
}
