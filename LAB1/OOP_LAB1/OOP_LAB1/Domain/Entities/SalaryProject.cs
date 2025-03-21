
using OOP_LAB1.Domain.Enums;

namespace OOP_LAB1.Domain.Entities;

public class SalaryProject
{
    public int Id { get; set; }
    public int EnterpriseId { get; set; }
    
    public decimal Balance { get; set; }
    public int BankId { get; set; }
    public SalaryProjectStatus Status { get; set; } = SalaryProjectStatus.Application;

    public void Activate()
    {
        Status = SalaryProjectStatus.Active;
    }

    public void Reject()
    {
        Status = SalaryProjectStatus.Rejected;
    }

    public void Deposit(decimal amount)
    {
        Balance += amount;
    }

    public void Withdraw(decimal amount)
    {
        Balance -= amount;
    }

    public void Block()
    {
        Status = SalaryProjectStatus.Blocked;
    }

    public void Unblock()
    {
        Status = SalaryProjectStatus.Blocked;
    }
}

