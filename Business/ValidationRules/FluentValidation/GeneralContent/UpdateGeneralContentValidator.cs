using Entities.DTOs.GeneralContent;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.GeneralContent
{
    public class UpdateGeneralContentValidator : AbstractValidator<UpdateGeneralContentDto>
    {
        public UpdateGeneralContentValidator()
        {
            RuleFor(g => g.Name).NotEmpty()
                .MaximumLength(255);

            RuleFor(g => g.Type).NotEmpty()
                .MaximumLength(20);

            RuleFor(g => g.Value).NotEmpty()
                .GreaterThanOrEqualTo(0);

            RuleFor(p => p.ImagePath).Length(0, 255);
        }
    }
}
