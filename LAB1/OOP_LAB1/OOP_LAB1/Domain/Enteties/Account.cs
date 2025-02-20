using OOP_LAB1.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace OOP_LAB1.Domain.Enteties
{
    internal class Account
    {
        public int Id;
        public decimal Balance;
        public AccountStatus Status;
        public DateTime CreatedDate;
        public int OwnerId;


        public void UpdateBalance(decimal newAmount)
        {
            if (Status != AccountStatus.Active)
            {
                throw new InvalidOperationException("Account not active");
            }
            Balance = newAmount;
        }
    }

    
}
