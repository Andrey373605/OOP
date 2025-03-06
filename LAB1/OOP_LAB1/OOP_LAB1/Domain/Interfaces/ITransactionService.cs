namespace OOP_LAB1.Domain.Interfaces;

public interface ITransactionService
{
    public Task WithdrawFunds(decimal amount, int accountId);
    public Task TransferFunds(decimal amount, int fromAccountId, int toAccountId);
    public Task DepositFunds(decimal amount, int accountId);
}