using OOP_LAB1.Domain.Entities;
namespace OOP_LAB1.Application.Context;

public class EmployeeContext
{
    public Employee CurrentEmployee { get; private set; }

    public void SetCurrent(Employee employee)
    {
        CurrentEmployee = employee;
    }

    public void ClearCurrent()
    {
        CurrentEmployee = null;
    }
}