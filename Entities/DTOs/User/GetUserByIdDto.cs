using Core.Entities.Abstract;

namespace Entities.DTOs.User
{
    public class GetUserByIdDto :IDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}
