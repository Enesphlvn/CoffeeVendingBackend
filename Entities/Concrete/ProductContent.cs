using Core.Entities.Concrete;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    [Table("ProductContents")]
    public class ProductContent : BaseEntity
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int GeneralContentId { get; set; }
        public int Unit { get; set; }
    }
}
