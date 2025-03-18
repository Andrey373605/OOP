
using OOP_LAB1.Domain.Enums;

namespace OOP_LAB1.Domain.Entities;

public class SalaryProject
{
    public int Id { get; set; }
    public int EnterpriseId { get; set; }
    
    public int Balance { get; set; }
    public int BankId { get; set; }
    public SalaryProjectStatus Status { get; set; }

    public void Activate()
    {
        Status = SalaryProjectStatus.Active;
    }

    public void Reject()
    {
        Status = SalaryProjectStatus.Rejected;
    }
}

