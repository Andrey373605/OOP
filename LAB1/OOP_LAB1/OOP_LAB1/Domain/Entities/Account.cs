using OOP_LAB1.Domain.Enums;

namespace OOP_LAB1.Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public int OwnerId { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsFrozen { get; set; }
        
        public AccountType AccountType { get; set; }

        public void UpdateBalance(decimal newAmount)
        {
            if (!IsBlocked && !IsFrozen)
            {
                throw new InvalidOperationException("Account not active");
            }
            Balance = newAmount;
        }

        public void FreezeAccount()
        {
            IsFrozen = true;
        }
        
        public void UnfreezeAccount()
        {
            IsFrozen = false;
        }

        public void BlockAccount()
        {
            IsBlocked = true;
        }

        public void UnblockAccount()
        {
            IsBlocked = false;
        }

        public void DepositAccount(decimal amount)
        {
            Balance += amount;
        }
    }

    
}
