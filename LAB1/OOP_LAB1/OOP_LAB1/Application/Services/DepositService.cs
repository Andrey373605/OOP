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
    
    public void CreateDepositAccount(int userId, decimal depositAmount, decimal interestRate)
    {
        _depositRepository.Add(userId, depositAmount, interestRate);
    }

    public void DepositMoney(decimal depositAmount)
    {
        throw new NotImplementedException();
    }

    public Task AddDepositRequest()
    {
        throw new NotImplementedException();
    }

    public Task AddDepositRequest(int idUser, decimal depositAmount, decimal interestRate, int monthCount)
    {
        DepositRequest depositRequest = new DepositRequest
        {
            UserId = idUser,
            Amount = depositAmount,
            InterestRate = interestRate,
            MonthCount = monthCount
            
        };

        return _depositRepository.CreateRequestAsync(depositRequest);
    }
}