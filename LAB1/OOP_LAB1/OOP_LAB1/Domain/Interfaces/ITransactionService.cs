namespace OOP_LAB1.Domain.Interfaces;

public interface ITransactionService
{
    public Task<(bool, string)> WithdrawFunds(decimal amount, int accountId);
    public Task<(bool, string)> TransferFunds(decimal amount, int fromAccountId, int toAccountId);
    public Task<(bool, string)> DepositFunds(decimal amount, int accountId);
}