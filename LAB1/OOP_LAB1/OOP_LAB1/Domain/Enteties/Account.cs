using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LAB1.Enteties
{
    internal class Account
    {
        public int Id;
        public string AccountNumber;
        public decimal Balance;
        public AccountStatus Status;
        public DateTime CreatedDate;
        public int OwnerId;
    }

    public enum AccountStatus
    {
        Active,
        Blocked,
        Frozen,
        Closed
    }
}
