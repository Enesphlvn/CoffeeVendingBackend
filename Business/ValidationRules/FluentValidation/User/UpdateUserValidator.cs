using Entities.DTOs.User;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.User
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserDto>
    {
        public UpdateUserValidator()
        {
            RuleFor(u => u.Id).NotEmpty()
                .GreaterThan(0);

            RuleFor(u => u.FirstName).NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);

            RuleFor(u => u.LastName).NotEmpty()
                .MinimumLength(2)
                .MaximumLength(50);

            RuleFor(u => u.Email).NotEmpty()
                .EmailAddress();
        }
    }
}
