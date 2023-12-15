using Entities.DTOs.UserOperationClaim;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.UserOperationClaim
{
    public class CreateUserOperationClaimValidator : AbstractValidator<CreateUserOperationClaimDto>
    {
        public CreateUserOperationClaimValidator()
        {
            RuleFor(o => o.UserId).NotEmpty()
                .GreaterThan(0);

            RuleFor(o => o.OperationClaimId).NotEmpty()
                .GreaterThan(0);
        }
    }
}
