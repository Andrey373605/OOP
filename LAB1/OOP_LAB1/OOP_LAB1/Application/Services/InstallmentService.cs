using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Domain.Interfaces;

namespace OOP_LAB1.Application.Services;

public class InstallmentService : IInstallmentService
{
    readonly IInstallmentRepository _installmentRepository;
    readonly IAccountRepository _accountRepository;
    readonly IClientRepository _clientRepository;

    public InstallmentService(IInstallmentRepository installmentRepository, IAccountRepository accountRepository, IClientRepository clientRepository)
    {
        _installmentRepository = installmentRepository;
        _accountRepository = accountRepository;
        _clientRepository = clientRepository;
    }
    
    
    public async Task DepositMoney(int installmentId)
    {
        var loan = await _installmentRepository.GetByIdAsync(installmentId);
        
        var account = await _accountRepository.GetByIdAsync(loan.AccountId);

        var sum = loan.CalculateMonthlyPayment();
        account.WithdrawAccount(sum);
        loan.DecreaseRestMonth();
        
        await _accountRepository.UpdateAsync(account);
        await _installmentRepository.UpdateAsync(loan);
    }

    public async Task AddInstallmentRequest(int clientId, decimal depositAmount, int monthCount)
    {
        var client = await _clientRepository.GetByIdAsync(clientId);
        if (client == null)
        {
            throw new ApplicationException($"Client with id {clientId} not found");
        }
        
        var installmentRequest = new Installment
        {
            Amount = depositAmount,
            ClientId = client.Id,
            NumberOfPayments = monthCount,
            IsActive = false
        };
        
        await _installmentRepository.AddAsync(installmentRequest);
    }

    public async Task ApproveInstallmentRequest(int installmentId)
    {
        var installmentRequest = await _installmentRepository.GetByIdAsync(installmentId);
        
        var account = new Account
        {
            Balance = 0,
            IsBlocked = false,
            IsFrozen = false,
            AccountType = AccountType.Installment,
            ClientId = installmentRequest.ClientId,
        };
        installmentRequest.SetActive();

        await _accountRepository.AddAsync(account);
        await _installmentRepository.UpdateAsync(installmentRequest);
        
    }
}

