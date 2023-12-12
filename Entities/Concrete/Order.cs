using Core.Entities.Concrete;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    [Table("Orders")]
    public class Order : BaseEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int AmountPaid { get; set; }
        public int RefundPaid { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
