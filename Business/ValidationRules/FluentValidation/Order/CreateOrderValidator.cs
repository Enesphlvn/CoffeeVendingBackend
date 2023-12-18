using Entities.DTOs.Order;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.Order
{
    public class CreateOrderValidator : AbstractValidator<CreateOrderDto>
    {
        public CreateOrderValidator()
        {
            RuleFor(o => o.ProductId).NotEmpty()
                .GreaterThan(0);

            RuleFor(o => o.UserId).NotEmpty()
                .GreaterThan(0);

            RuleFor(o => o.AmountPaid).NotEmpty()
                .GreaterThan(0);
        }
    }
}
