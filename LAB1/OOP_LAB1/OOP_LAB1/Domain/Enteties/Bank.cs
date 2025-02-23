
namespace OOP_LAB1.Domain.Enteties
{
    public class Bank
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<User> Users { get; set; } = new();
        public List<Enterprise> Enterprises { get; set; } = new();
    }
}
