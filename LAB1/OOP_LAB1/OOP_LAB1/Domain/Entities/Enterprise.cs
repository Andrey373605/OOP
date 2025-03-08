

namespace OOP_LAB1.Domain.Entities
{
    public class Enterprise
    {
        public int Id { get; set; }
        public string Type { get; set; } = null!;
        public string LegalName { get; set; } = null!;
        public string UNP { get; set; } = null!;
        public string Address { get; set; } = null!;
        public int BankId { get; set; }
    }
}
