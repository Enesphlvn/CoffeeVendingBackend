using Core.Entities.Abstract;

namespace Entities.DTOs.Statistics
{
    public class UserStatistics : IDto
    {
        public string UserName { get; set; }
        public int Quantity { get; set; }
    }
}
