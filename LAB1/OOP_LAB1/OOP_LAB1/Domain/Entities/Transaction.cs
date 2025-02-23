using OOP_LAB1.Domain.Enums;

namespace OOP_LAB1.Domain.Entities
{
    internal class Transaction
    {
        public int Id { get; set; }
        public Account FromAccount { get; set; }
        public Account ToAccount { get; set; }
        public decimal Amount { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public TransactionType Type { get; set; }
    }
}
