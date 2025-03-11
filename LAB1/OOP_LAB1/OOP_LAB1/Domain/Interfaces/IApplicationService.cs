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
    Task RegisterEmployee(UserRole role);
    Task CreateAccount();
    Task DepositAccount(int accountId, decimal sum);
    Task TransferAccount(int fromAccountId, int toAccountId, decimal sum);
    Task FreezeAccount(int accountId);
    
    Task UnfreezeAccount(int accountId);
    Task CreateInstallmentRequest(decimal sum, int duration);
    Task CreateLoanRequest(decimal sum, int rate, int duration);
}