using Entities.DTOs.Product;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.Product
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductDto>
    {
        public UpdateProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty()
                .Length(5, 100);

            RuleFor(p => p.Price).NotEmpty()
                .GreaterThanOrEqualTo(0)
                .LessThanOrEqualTo(1000);

            RuleFor(p => p.Description).MaximumLength(500);

            RuleFor(p => p.ImagePath).Length(0, 255)
                .Must(path => path.ToLower().EndsWith(".jpg"));
        }
    }
}
