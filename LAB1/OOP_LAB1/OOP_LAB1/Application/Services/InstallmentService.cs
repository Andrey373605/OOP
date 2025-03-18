using OOP_LAB1.Application.Context;
using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Infrastructure.Data;

namespace OOP_LAB1.Application.Services;

public class InstallmentService : IInstallmentService
{
    private readonly IInstallmentRepository _installmentRepository;
    private readonly IAccountRepository _accountRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IBankRepository _bankRepository;
    private readonly IDataBaseHelper _dataBaseHelper;

    public InstallmentService(IInstallmentRepository installmentRepository, IAccountRepository accountRepository,
        IClientRepository clientRepository, IBankRepository bankRepository, IDataBaseHelper dataBaseHelper)
    {
        _installmentRepository = installmentRepository;
        _accountRepository = accountRepository;
        _clientRepository = clientRepository;
        _bankRepository = bankRepository;
        _dataBaseHelper = dataBaseHelper;
    }
    
    
    public async Task DepositMoney(int installmentId)
    {
        var installment = await _installmentRepository.GetByIdAsync(installmentId);
        
        var account = await _accountRepository.GetByIdAsync(installment.AccountId);

        var sum = installment.CalculateMonthlyPayment();
        account.WithdrawAccount(sum);
        installment.DecreaseRestMonth();
        
        await _accountRepository.UpdateAsync(account);
        await _installmentRepository.UpdateAsync(installment);
    }

    public async Task CreateInstallmentRequest(int  clientId, decimal depositAmount, int monthCount)
    {
        var client = await _clientRepository.GetByIdAsync(clientId);
        if (client == null)
        {
            throw new ApplicationException("Client not found");
        }

        var account = new Account
        {
            Balance = 0,
            AccountType = AccountType.Loan,
            Status = AccountStatus.Blocked,
            ClientId = client.Id,
            BankId = client.BankId
        };
        
        await _accountRepository.AddAsync(account);
        var accointId = _dataBaseHelper.GetLastInsertId();
        

        Installment installmentRequest = new Installment
        {
            ClientId = client.Id,
            AccountId = accointId,
            NumberOfPayments = monthCount,
            RestMonth = monthCount,
            Amount = depositAmount,
            Status = InstallmentStatus.Application
        };

        await _installmentRepository.AddAsync(installmentRequest);
    }

    public async Task ApproveInstallmentRequest(int installmentId)
    {
        var installmentRequest = await _installmentRepository.GetByIdAsync(installmentId);
        if (installmentRequest == null)
        {
            throw new ApplicationException($"Loan request with id: {installmentId} does not exist");
        }
        
        installmentRequest.Activate();
        
        var account = await _accountRepository.GetByIdAsync(installmentRequest.AccountId);
        account.DepositAccount(installmentRequest.Amount);
        account.Status = AccountStatus.Active;

        await _accountRepository.UpdateAsync(account);
        await _installmentRepository.UpdateAsync(installmentRequest);
        
    }

    public async Task RejectInstallmentRequest(int installmentId)
    {
        var installmentRequest = await _installmentRepository.GetByIdAsync(installmentId);
        if (installmentRequest == null)
        {
            throw new ApplicationException($"Loan request with id: {installmentId} does not exist");
        }
        
        installmentRequest.Reject();
        
        await _installmentRepository.UpdateAsync(installmentRequest);
    }

    public async Task<IEnumerable<Installment>> GetAllClientInstallmentsAsync(int clientId)
    {
        return await _installmentRepository.GetAllByClientId(clientId);
    }

    public async Task<IEnumerable<Installment>> GetInstallmentApplicationsAsync()
    {
        return await _installmentRepository.GetInstallmentApplications();
    }
}

