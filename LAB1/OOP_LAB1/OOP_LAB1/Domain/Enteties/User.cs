using OOP_LAB1.Domain.Enums;

namespace OOP_LAB1.Domain.Enteties
{
    internal class User
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public string PassportSeries { get; set; }
        public string IdentificationNumber { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public bool IsForeigner { get; set; }
        public UserRole Role { get; set; }
        public List<Account> Accounts { get; set; } = new();
    }
}
