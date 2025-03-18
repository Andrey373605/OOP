using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Domain.Interfaces;

public interface ITransactionService
{
    public Task<bool> WithdrawFunds(decimal amount, int accountId);
    public Task<bool> TransferFunds(decimal amount, int fromAccountId, int toAccountId);
    public Task<bool> DepositFunds(decimal amount, int accountId);
    public Task<bool> CancelTransfer(int transferId);
    
    public Task<Transaction> GetTransferById(int transferId);
    public Task<IEnumerable<Transaction>> GetTransferByAccountId(int accountId);
    public Task<IEnumerable<Transaction>> GetDepositByAccountId(int accountId);
    public Task<IEnumerable<Transaction>> GetWithdrawByAccountId(int accountId);
}