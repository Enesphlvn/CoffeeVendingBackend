using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Entities.Concrete
{
    [Table("OperationClaims")]
    public class OperationClaim : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}