
using OOP_LAB1.Domain.Enums;

namespace OOP_LAB1.Domain.Entities
{
    public class Installment
    {
        public int Id { get; set; }
        public int ClientId { get; init; }
        public int AccountId { get; set; }
        public decimal Amount { get; init; }
        public int NumberOfPayments { get; init; }
        
        public DateTime StartDate { get; set; }
        
        public int RestMonth { get; set; }
        public InstallmentStatus Status { get; set; } = InstallmentStatus.Application;
        
        public void DecreaseRestMonth()
        {
            RestMonth--;
        }

        public void Activate()
        {
            Status = InstallmentStatus.Active;
        }

        public void Close()
        {
            Status = InstallmentStatus.Closed;
        }
        
        public decimal CalculateMonthlyPayment()
        {
            return Amount / NumberOfPayments;
        }

        public void Reject()
        {
            Status = InstallmentStatus.Rejected;
        }
        
    }
}
