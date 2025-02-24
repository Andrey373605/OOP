using OOP_LAB1.Domain.Enums;

namespace OOP_LAB1.Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public AccountType Type { get; set; }
        public int OwnerId { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsFrozen { get; set; }
        public bool IsUserOwner { get; set; }
        public bool IsEnterpriseOwner { get; set; }
        public void UpdateBalance(decimal newAmount)
        {
            if (!IsBlocked && !IsFrozen)
            {
                throw new InvalidOperationException("Account not active");
            }
            Balance = newAmount;
        }
    }

    
}
