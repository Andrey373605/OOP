using OOP_LAB1.Domain.Enums;
namespace OOP_LAB1.Domain.Entities;

public class Employee
{
    public int Id { get; set; }
    public string Email { get; set; }
    public string HashPassword { get; set; }
    public EmployeeRole Role { get; set; }
}