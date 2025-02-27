namespace OOP_LAB1.Domain.Entities;

public class RegistrationRequest
{
    public int Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public string HashPassword;
    public string PassportSeries { get; set; }
    public string IdentificationNumber { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}