namespace OOP_LAB1.Domain.Interfaces;

public interface ITransactionService
{
    public Task<bool> WithdrawFunds(decimal amount, int accountId);
    public Task<bool> TransferFunds(decimal amount, int fromAccountId, int toAccountId);
    public Task<bool> DepositFunds(decimal amount, int accountId);
}