namespace OOP_LAB1.Domain.Interfaces;

public interface IInstallmentService
{
    public Task DepositMoney(int installmentId);

    public Task AddInstallmentRequest(int idUser, decimal depositAmount, int interestRate, int monthCount);

    public Task ApproveInstallmentRequest(int loanId);
}