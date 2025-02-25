using OOP_LAB1.Domain.Enums;

namespace OOP_LAB1.Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public int OwnerId { get; set; }
        private bool IsBlocked;
        private bool IsFrozen;
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
    }

    
}
