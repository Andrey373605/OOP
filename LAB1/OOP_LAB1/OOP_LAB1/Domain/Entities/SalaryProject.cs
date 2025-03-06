
namespace OOP_LAB1.Domain.Entities;

public class SalaryProject
{
    public int Id { get; set; }
    public int EnterpriseId { get; set; }
    
    public int AccountId { get; set; }
    public int BankId { get; set; }
    public bool IsActive { get; set; }

    public void SetActive()
    {
        IsActive = true;
    }

    public void SetInactive()
    {
        IsActive = false;
    }
}

