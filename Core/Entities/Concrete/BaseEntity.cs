using Core.Entities.Abstract;

namespace Core.Entities.Concrete
{
    public class BaseEntity : IEntity
    {
        public DateTime CreatedAt { get; set; }
        public bool IsStatus { get; set; }
    }
}
