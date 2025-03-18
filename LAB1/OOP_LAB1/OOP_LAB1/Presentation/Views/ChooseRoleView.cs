using OOP_LAB1.Application.Context;
using OOP_LAB1.Application.Services;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.ChooseRolePage)]
public class ChooseRoleView : IView
{
    public PageName? NextViewName { get; private set; }
    

    private readonly IConsole _console;
    private readonly IInputHandler _inputHandler;


    public ChooseRoleView(IConsole console, IInputHandler inputHandler)
    {

        _console = console;
        _inputHandler = inputHandler;

    }
    public async Task Execute()
    {
        _console.WriteLine("Choose:");
        _console.WriteLine("1. Register as client");
        _console.WriteLine("2. Login as client");
        _console.WriteLine("3. Register as employee");
        _console.WriteLine("4. Login as employee");
        _console.WriteLine("5. Logout");
        _console.WriteLine("5. Exit");

        var choice = _inputHandler.GetNumberVariant(6);
        _console.Clear();
        NextViewName = choice switch
        {
            "1" => PageName.RegistrationClientPage,
            "2" => PageName.LoginClientPage,
            "3" => PageName.RegistrationEmployeePage,
            "4" => PageName.LoginEmployeePage,
            "5" => PageName.LogOutPage,
            "6" => PageName.ExitPage,
            _ => PageName.ChooseRolePage
        };

        
        


    }
}