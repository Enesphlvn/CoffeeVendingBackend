using Core.Entities.Abstract;

namespace Entities.DTOs.Order
{
    public class UpdateOrderDto : IDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int AmountPaid { get; set; }
        public int RefundPaid { get; set; }
    }
}
