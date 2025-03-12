using OOP_LAB1.Application.Context;
using OOP_LAB1.Application.Services;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;

namespace OOP_LAB1.Presentation.Views;

public class ChooseRoleView : IView
{
    public PageName? NextViewName { get; private set; }
    

    private readonly IConsole _console;


    public ChooseRoleView(IConsole console)
    {

        _console = console;

    }
    public async Task Execute()
    {
        _console.WriteLine("Choose:");
        _console.WriteLine("1. Register as client");
        _console.WriteLine("2. Login as client");
        _console.WriteLine("3. Register as employee");
        _console.WriteLine("4. Login as employee");
        _console.WriteLine("5. Exit");
        
        var choice = _console.ReadLine();
        NextViewName = choice switch
        {
            "1" => PageName.RegistrationClientPage,
            "2" => PageName.LoginClientPage,
            "3" => PageName.RegistrationEmployeePage,
            "4" => PageName.LoginEmployeePage,
            "5" => PageName.ExitPage,
            _ => PageName.ChooseRolePage
        };

        
        


    }
}