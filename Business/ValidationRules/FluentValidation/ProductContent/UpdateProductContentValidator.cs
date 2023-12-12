using Entities.DTOs.ProductContent;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.ProductContent
{
    public class UpdateProductContentValidator : AbstractValidator<UpdateProductContentDto>
    {
        public UpdateProductContentValidator()
        {
            RuleFor(p => p.ProductId).NotEmpty()
                .GreaterThan(0);

            RuleFor(p => p.GeneralContentId).NotEmpty()
                .GreaterThan(0);

            RuleFor(p => p.Unit).GreaterThan(0);
        }
    }
}
