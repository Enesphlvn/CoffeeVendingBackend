using Entities.DTOs.OperationClaim;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.OperationClaim
{
    public class UpdateOperationClaimValidator : AbstractValidator<UpdateOperationClaimDto>
    {
        public UpdateOperationClaimValidator()
        {
            RuleFor(o => o.Id).NotEmpty()
                .GreaterThan(0);

            RuleFor(o => o.Name).NotEmpty()
                .Length(3, 100);
        }
    }
}
