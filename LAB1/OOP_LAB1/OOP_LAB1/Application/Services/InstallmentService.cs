using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Domain.Interfaces;

namespace OOP_LAB1.Application.Services;

public class InstallmentService : IInstallmentService
{
    readonly IInstallmentRepository _installmentRepository;
    readonly IAccountRepository _accountRepository;

    public InstallmentService(IInstallmentRepository installmentRepository, IAccountRepository accountRepository)
    {
        _installmentRepository = installmentRepository;
        _accountRepository = accountRepository;
    }
    
    
    public Task DepositMoney(int installmentId)
    {
        throw new NotImplementedException();
    }

    public async Task AddInstallmentRequest(int idUser, decimal depositAmount, int monthCount)
    {
        var installmentRequest = new Installment
        {
            Amount = depositAmount,
            UserId = idUser,
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
            OwnerId = installmentRequest.UserId,
        };
        installmentRequest.SetActive();

        await _accountRepository.AddAsync(account);
        await _installmentRepository.UpdateAsync(installmentRequest);
        
    }
}

