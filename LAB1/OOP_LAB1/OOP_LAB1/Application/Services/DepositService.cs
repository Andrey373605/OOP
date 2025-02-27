using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Interfaces;

namespace OOP_LAB1.Application.Services;

public class DepositService : IDepositService
{
    readonly IDepositRepository _depositRepository;

    DepositService(IDepositRepository depositRepository)
    {
        _depositRepository = depositRepository;
    }
    
    public async Task ApproveDepositAsync(int depositId)
    {
        Deposit deposit = await _depositRepository.GetByIdAsync(depositId);
        deposit.SetActive();
        await _depositRepository.UpdateAsync(deposit);
    }


    public async Task DepositMoneyAsync(int id, decimal depositAmount)
    {
        Deposit deposit = await _depositRepository.GetByIdAsync(id);
        deposit.MakeDeposit(depositAmount);
        await _depositRepository.UpdateAsync(deposit);
    }

    
    public async Task AddAsync(int idUser, decimal depositAmount, decimal interestRate, int monthCount)
    {
        Deposit depositRequest = new Deposit
        {
            UserId = idUser,
            Amount = depositAmount,
            InterestRate = interestRate,
            MonthCount = monthCount,
            IsActive = false
            
        };

        await _depositRepository.AddAsync(depositRequest);
    }
}