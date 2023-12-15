using Entities.DTOs.User;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.User
{
    public class PasswordUpdateValidator : AbstractValidator<PasswordUpdateDto>
    {
        public PasswordUpdateValidator()
        {
            RuleFor(u => u.Email).NotEmpty()
                .EmailAddress();

            RuleFor(u => u.OldPassword).NotEmpty()
                .MinimumLength(5)
                .MaximumLength(100);

            RuleFor(u => u.NewPassword).NotEmpty()
                .MinimumLength(5)
                .MaximumLength(100)
                .NotEqual(x => x.OldPassword);
        }
    }
}
