namespace OOP_LAB1.Domain.Interfaces;

public interface ILoanService
{
    public void CreateLoanAccount(int userId, decimal depositAmount, decimal interestRate);
    
    public void DepositMoney(decimal depositAmount);

    public Task AddLoanRequest(int idUser, decimal depositAmount, decimal interestRate, int monthCount);
}