using Entities.DTOs.User;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.User
{
    public class LoginUserValidator : AbstractValidator<LoginUserDto>
    {
        public LoginUserValidator()
        {
            RuleFor(u => u.Email).NotEmpty()
                .EmailAddress();

            RuleFor(u => u.Password).NotEmpty()
                .MinimumLength(5)
                .MaximumLength(100);
        }
    }
}
