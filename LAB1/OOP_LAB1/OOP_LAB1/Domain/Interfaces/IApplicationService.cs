using System.Collections;
using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;

namespace OOP_LAB1.Domain.Interfaces;

public interface IApplicationService
{
    public Task LoginUser(string email, string password);

    public Task<IEnumerable<Account>> GetCurrentClientAccounts();
    void LoginBank(Bank bank);
    Task LoginClient();
    Task RegisterClient(string firstName, string lastName, string middleName, string phoneNumber, string identificationNumber, string series);
    Task RegisterEmployee(EmployeeRole role);
    Task CreateAccount();
    Task DepositAccount(int accountId, decimal sum);
    Task TransferAccount(int fromAccountId, int toAccountId, decimal sum);
    Task WithdrawAccount(int accountId, decimal sum);
    Task FreezeAccount(int accountId);
    
    Task UnfreezeAccount(int accountId);
    Task CreateInstallmentRequest(decimal sum, int duration);
    Task CreateLoanRequest(decimal sum, int rate, int duration);
    Task LoginEmployee();
    Task<EmployeeRole> GetCurrentEmployeeRole();
    Task<IEnumerable<Loan>> GetCurrentClientLoans();
    Task<IEnumerable<Installment>> GetCurrentClientInstallments();
    Task<IEnumerable<Transaction>> GetTransfersByAccountIdAsync(int accountId);
    Task<IEnumerable<Transaction>> GetDepositsByAccountIdAsync(int accountId);
    Task<IEnumerable<Transaction>> GetWithdrawsByAccountIdAsync(int accountId);
    Task LogOutUser();
}