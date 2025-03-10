namespace OOP_LAB1.Domain.Entities;

public class Client
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    public int BankId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    
    public string PassportSeries { get; set; }
    public string IdentificationNumber { get; set; }
    public string Phone { get; set; }
    public bool IsActive { get; private set; } = false;

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
    
}