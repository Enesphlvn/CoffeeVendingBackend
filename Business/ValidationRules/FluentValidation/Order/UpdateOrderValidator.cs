using Entities.DTOs.Order;
using FluentValidation;

namespace Business.ValidationRules.FluentValidation.Order
{
    public class UpdateOrderValidator : AbstractValidator<UpdateOrderDto>
    {
        public UpdateOrderValidator()
        {
            RuleFor(o => o.ProductId).NotEmpty()
                .GreaterThan(0);

            RuleFor(o => o.AmountPaid).NotEmpty()
                .GreaterThan(0);

            RuleFor(o => o.RefundPaid).GreaterThanOrEqualTo(0)
                .LessThan(o => o.AmountPaid)
                .When(o => o.AmountPaid > 0);
        }
    }
}
