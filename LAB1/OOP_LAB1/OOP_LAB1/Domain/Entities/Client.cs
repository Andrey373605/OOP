using OOP_LAB1.Domain.Enums;

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
    public ClientStatus Status { get; set; }

    public void Activate()
    {
        Status = ClientStatus.Active;
    }

    public void Delete()
    {
        Status = ClientStatus.Deleted;
    }

    public bool IsActive()
    {
        return Status == ClientStatus.Active;
    }

    public void Reject()
    {
        Status = ClientStatus.Rejected;
    }
}