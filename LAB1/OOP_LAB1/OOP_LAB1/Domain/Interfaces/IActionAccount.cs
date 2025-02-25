namespace OOP_LAB1.Domain.Interfaces;

public interface IActionAccount
{
    public void WithdrawFunds(decimal amount);
    public void TransferFunds(decimal amount, string destination);
    public void DepositFunds(decimal amount);
}