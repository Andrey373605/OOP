using OOP_LAB1.Domain.Enteties;
using OOP_LAB1.Domain.Enums;


namespace OOP_LAB1.Domain.Interfaces
{
    internal interface IAccountService
    {
        Account CreateAccount(AccountType type, int ownerId);

        void BlockAccount(int accountId);

        void UnblockAccount(int accountId);

        void WithdrawFunds(int accountId, decimal amount);

        void DepositFunds(int accountId, decimal amount);

        void TransferFunds(int fromAccountId, int toAccountId, decimal amount);

        Account GetAccountById(int id);

        IEnumerable<Account> GetAccountsByOwnerId(int ownerId);
    }
}
