using Entities.DTOs.ProductContent;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.ProductContent
{
    public class CreateProductContentValidator : AbstractValidator<CreateProductContentDto>
    {
        public CreateProductContentValidator()
        {
            RuleFor(p => p.ProductId).NotEmpty()
                .GreaterThan(0);

            RuleFor(p => p.GeneralContentId).NotEmpty()
                .GreaterThan(0);

            RuleFor(p => p.Unit).GreaterThan(0);
        }
    }
}
