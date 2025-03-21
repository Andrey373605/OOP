using System.ComponentModel.DataAnnotations;
using OOP_LAB1.Domain.Enums;

namespace OOP_LAB1.Domain.Entities;

public class Employee
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int BankId { get; set; }
    public EmployeeRole Role { get; set; }

    public EmployeeStatus Status { get; set; } = EmployeeStatus.Application;

    public void Activate()
    {
        Status = EmployeeStatus.Active;
    }

    public void Reject()
    {
        Status = EmployeeStatus.Rejected;
    }
}