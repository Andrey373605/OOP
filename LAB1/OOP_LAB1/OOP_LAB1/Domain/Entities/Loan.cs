using OOP_LAB1.Domain.Enums;

namespace OOP_LAB1.Domain.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public int MonthCount { get; set; }
        public decimal InterestRate { get; set; }
        public decimal MonthlyPayment { get; set; }
        public bool IsActive { get; set; }
    }
}
