using Core.Entities.Abstract;
using Entities.DTOs.Product;

namespace Entities.DTOs.Order
{
    public class GetOrderByIdDto : IDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int ProductId { get; set; }
        public int AmountPaid { get; set; }
        public int RefundPaid { get; set; }

        public ICollection<GetProductDto> Products { get; set; }
    }
}
