using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Domain.Interfaces;

public interface IInstallmentService
{
    public Task DepositMoney(int installmentId);
    
    public Task PayInstallment(int installmentId);

    public Task CreateInstallmentRequest(int clientId, decimal depositAmount, int monthCount);

    public Task ApproveInstallmentRequest(int installmentId);
    public Task RejectInstallmentRequest(int installmentId);
    Task<IEnumerable<Installment>> GetAllClientInstallments(int clientId);
    
    Task<IEnumerable<Installment>> GetInstallmentApplications();

    Task PayAll();
}