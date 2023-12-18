using Entities.DTOs.User;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.User
{
    public class RegisterUserValidator : AbstractValidator<RegisterUserDto>
    {
        public RegisterUserValidator()
        {
            RuleFor(u => u.Email).NotEmpty()
                .EmailAddress();

            RuleFor(u => u.FirstName).NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);

            RuleFor(u => u.LastName).NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);

            RuleFor(u => u.Password).NotEmpty()
                .MinimumLength(5)
                .MaximumLength(100);

            
        }
    }
}
