using OOP_LAB1.Domain.Enums;

namespace OOP_LAB1.Domain.Enteties
{
    internal class Loan
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public int TermInMonths { get; set; }
        public decimal InterestRate { get; set; }
        public decimal MonthlyPayment { get; set; }
        public LoanStatus Status { get; set; }
    }
}
