using Entities.DTOs.Product;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.Product
{
    public class CreateProductValidator : AbstractValidator<CreateProductDto>
    {
        public CreateProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty()
                .Length(3, 100);

            RuleFor(p => p.Price).NotEmpty()
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(1000);

            RuleFor(p => p.Description).MaximumLength(500);

            RuleFor(p => p.ImagePath).MaximumLength(250)
                .Must(path => path.ToLower().EndsWith(".jpg"));
        }
    }
}
