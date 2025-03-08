using OOP_LAB1.Domain.Enums;

namespace OOP_LAB1.Domain.Entities
{
    public class Loan
    {
        public int Id { get; set; }
        
        public int AccountId { get; set; }
        public int UserId { get; set; }
        public decimal Amount { get; set; }
        public int NumberOfPayments { get; set; }
        public int InterestRate { get; set; }

        public decimal RestMonth { get; set; }
        public bool IsActive { get; set; }
        
        public decimal CalculateMonthlyPayment()
        {
            return Amount * (1 + ((decimal)InterestRate / 100)) / NumberOfPayments;
        }

        public void SetActive()
        {
            IsActive = true;
        }

        public void SetInactive()
        {
            IsActive = false;
        }
        
    }
}
