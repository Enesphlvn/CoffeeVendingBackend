using Core.Entities.Abstract;

namespace Entities.DTOs.Order
{
    public class CreateOrderDto : IDto
    {
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int AmountPaid { get; set; }
        public int RefundPaid { get; set; }
    }
}
