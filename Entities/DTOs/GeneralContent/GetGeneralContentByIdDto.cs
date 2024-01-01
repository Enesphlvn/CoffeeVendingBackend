using Core.Entities.Abstract;

namespace Entities.DTOs.GeneralContent
{
    public class GetGeneralContentByIdDto : IDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int Value { get; set; }
        public bool IsCritialLevel { get; set; }
        public string ImagePath { get; set; }
    }
}
