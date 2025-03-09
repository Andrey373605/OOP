using OOP_LAB1.Application.Context;

namespace OOP_LAB1.Domain.Interfaces;

public interface IInstallmentService
{
    public Task DepositMoney(int installmentId);

    public Task CreateInstallmentRequest(IContext context, decimal depositAmount, int monthCount);

    public Task ApproveInstallmentRequest(int loanId);
}