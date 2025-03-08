
namespace OOP_LAB1.Domain.Entities
{
    public class Installment
    {
        public int Id { get; set; }
        public int ClientId { get; init; }
        public int AccountId { get; set; }
        public decimal Amount { get; init; }
        public int NumberOfPayments { get; init; }
        
        public int RestMonth { get; set; }
        public bool IsActive { get; set; }
        
        public void DecreaseRestMonth()
        {
            RestMonth--;
        }

        public void SetActive()
        {
            IsActive = true;
        }

        public void SetInactive()
        {
            IsActive = false;
        }
        
        public decimal CalculateMonthlyPayment()
        {
            return Amount / NumberOfPayments;
        }
        
    }
}
