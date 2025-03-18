using System.Diagnostics;
using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.RegistrationEmployeePage)]
public class RegistrationEmployeeView : IView
{
    private readonly IInputHandler _input;
    private readonly IConsole _console;
    private readonly IApplicationService _applicationService;
    
    public PageName? NextViewName { get; private set; }

    public RegistrationEmployeeView(IInputHandler input, IConsole console, IApplicationService applicationService)
    {
        _input = input;
        _console = console;
        _applicationService = applicationService;
    }

    public async Task Execute()
    {
        _console.WriteLine("Choose role:");
        _console.WriteLine("1. Operator");
        _console.WriteLine("2. Manager");
        _console.WriteLine("3. Specialist");
        _console.WriteLine("4. Administrator");
        
        string choice = _input.GetNumberVariant(4);
        EmployeeRole role = choice switch
        {
            "1" => EmployeeRole.Operator,
            "2" => EmployeeRole.Manager,
            "3" => EmployeeRole.ExternalSpecialist,
            "4" => EmployeeRole.Administrator,
            _ => EmployeeRole.Operator
        };
        
        _console.WriteLine($"Succesfully choosen role {role.ToString()}");
        
        _console.Clear();
        try
        {

            await _applicationService.RegisterEmployee(role);
            NextViewName = PageName.ChooseRolePage;
            _console.WriteLine("Registration successful!");
        }
        catch (Exception ex)
        {
            _console.WriteLine($"Error: {ex.Message}");
            NextViewName = PageName.ChooseRolePage;
        }
        
    }
}