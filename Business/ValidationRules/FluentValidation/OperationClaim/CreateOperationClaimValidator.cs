using Entities.DTOs.OperationClaim;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.OperationClaim
{
    public class CreateOperationClaimValidator : AbstractValidator<CreateOperationClaimDto>
    {
        public CreateOperationClaimValidator()
        {
            RuleFor(o => o.Name).NotEmpty()
                .Length(3, 100);
        }
    }
}
