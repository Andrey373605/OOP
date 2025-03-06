using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Domain.Interfaces;

public interface ILoanService
{
    public Task DepositMoney(int loanId);

    public Task AddLoanRequest(int idUser, decimal depositAmount, int interestRate, int monthCount);

    public Task ApproveLoanRequest(int loanId);
}