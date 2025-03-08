
namespace OOP_LAB1.Domain.Entities
{
    public class Installment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int AccountId { get; set; }
        public decimal Amount { get; set; }
        public int NumberOfPayments { get; set; }
        
        public int RestMonth { get; set; }
        public bool IsActive { get; set; }
        
        public void DecreaseRestMonth()
        {
            RestMonth = RestMonth - 1;
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
