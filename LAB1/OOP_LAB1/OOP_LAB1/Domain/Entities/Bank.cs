
namespace OOP_LAB1.Domain.Entities
{
    public class Bank
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required List<int> UsersIdList { get; set; }
        public required List<int> EnterprisesIdList { get; set; }
    }
}
