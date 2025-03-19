using OOP_LAB1.Domain.Enums;

namespace OOP_LAB1.Domain.Entities;

public class Salary
{
    public int Id { get; set; }
    public int AccountId { get; set; }
    public int SalaryProjectId { get; set; }
    public decimal Amount { get; set; }
    
    public SalaryStatus Status { get; set; }

    public void Activate()
    {
        Status = SalaryStatus.Active;
    }

    public void Reject()
    {
        Status = SalaryStatus.Rejected;
    }
}