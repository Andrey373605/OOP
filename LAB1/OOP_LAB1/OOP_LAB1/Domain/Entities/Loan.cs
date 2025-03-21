using OOP_LAB1.Domain.Enums;

namespace OOP_LAB1.Domain.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        
        public int AccountId { get; set; }
        public int ClientId { get; set; }
        public decimal Amount { get; set; }
        public int NumberOfPayments { get; set; }
        public int InterestRate { get; set; }

        public decimal RestMonth { get; set; }
        public LoanStatus Status { get; set; } = LoanStatus.Application;
        
        public decimal CalculateMonthlyPayment()
        {
            return Amount * (1 + ((decimal)InterestRate / 100)) / NumberOfPayments;
        }

        public void DecreaseRestMonth()
        {
            RestMonth--;
        }

        public void Activate()
        {
            Status = LoanStatus.Active;
        }

        public void Close()
        {
            Status = LoanStatus.Closed;
        }

        public void Reject()
        {
            Status = LoanStatus.Rejected;
        }
    }
}
