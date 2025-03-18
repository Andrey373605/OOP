using System.Collections;
using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Domain.Interfaces;

public interface ILoanService
{
    public Task DepositMoney(int loanId);

    public Task CreateLoanRequest(int clientId, decimal depositAmount, int interestRate, int monthCount);

    public Task ApproveLoanRequest(int loanId);
    Task<IEnumerable<Loan>> GetAllClientLoans(int clientId);
    
    Task<IEnumerable<Loan>> GetLoanApplications();
    Task RejectLoanRequest(int id);
}