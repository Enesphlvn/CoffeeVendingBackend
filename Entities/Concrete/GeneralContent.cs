using Core.Entities.Concrete;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities.Concrete
{
    [Table("GeneralContents")]
    public class GeneralContent : BaseEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Value { get; set; }
        public bool IsCritialLevel { get; set; }
    }
}
