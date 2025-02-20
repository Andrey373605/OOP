using OOP_LAB1.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace OOP_LAB1.Domain.Enteties
{
    internal class Account
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public AccountType Type { get; set; }
        public bool IsBlocked { get; set; }
        public bool IsFrozen { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }


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
