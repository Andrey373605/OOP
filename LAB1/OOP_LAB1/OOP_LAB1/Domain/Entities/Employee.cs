using OOP_LAB1.Domain.Enums;

namespace OOP_LAB1.Domain.Entities;

public class Employee
{
    public int Id { get; set; }
    public int UserId { get; set; }
    
    public int BankId { get; set; }
    public EmployeeRole Role { get; set; }
}