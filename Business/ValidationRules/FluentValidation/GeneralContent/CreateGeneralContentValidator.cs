using Entities.DTOs.GeneralContent;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.GeneralContent
{
    public class CreateGeneralContentValidator : AbstractValidator<CreateGeneralContentDto>
    {
        public CreateGeneralContentValidator()
        {
            RuleFor(g => g.Name).NotEmpty()
                .MaximumLength(50);

            RuleFor(g => g.Type).NotEmpty()
                .MaximumLength(30);

            RuleFor(g => g.Value).NotEmpty()
                .GreaterThanOrEqualTo(1000);

            RuleFor(p => p.ImagePath).MaximumLength(250);
        }
    }
}
