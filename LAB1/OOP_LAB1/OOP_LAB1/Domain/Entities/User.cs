using OOP_LAB1.Domain.Enums;

namespace OOP_LAB1.Domain.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string HashPassword;
        public bool IsForeigner { get; set; }
    }
}
