using Entities.DTOs.User;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.User
{
    public class GetUserByMailValidator : AbstractValidator<GetUserByMailDto>
    {
        public GetUserByMailValidator()
        {
            RuleFor(u => u.Email).NotEmpty()
                .EmailAddress();
        }
    }
}
