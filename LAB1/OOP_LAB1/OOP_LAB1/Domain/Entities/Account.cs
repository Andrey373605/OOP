using System.ComponentModel.DataAnnotations;
using OOP_LAB1.Domain.Enums;

namespace OOP_LAB1.Domain.Entities
{
    public class Account
    {
        public int Id { get; set; }

        public int BankId { get; set; }
        public decimal Balance { get; set; }
        public int ClientId { get; set; }
        
        public AccountStatus Status { get; set; }
        public AccountType AccountType { get; set; }
        

        public void FreezeAccount()
        {
            Status = AccountStatus.Frozen;
        }
        
        public void UnfreezeAccount()
        {
            Status = AccountStatus.Normal;
        }

        public void BlockAccount()
        {
            Status = AccountStatus.Blocked;
        }

        public void UnblockAccount()
        {
            Status = AccountStatus.Normal;
        }

        public void DepositAccount(decimal amount)
        {
            Balance += amount;
        }

        public void WithdrawAccount(decimal amount)
        {
            Balance -= amount;
        }
    }

    
}
