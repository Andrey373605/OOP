using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;

namespace OOP_LAB1.Presentation.Views;

public class ChooseRoleView : IView
{
    public PageName? NextViewName { get; private set; }
    
    private readonly IInputHandler _input;
    private readonly IAuthorizationService _auth;
    private readonly IConsoleView _console;
    private readonly IContext _context;

    public ChooseRoleView(IInputHandler input, IAuthorizationService auth, IConsoleView console, IContext context)
    {
        _input = input;
        _auth = auth;
        _console = console;
        _context = context;
    }
    public void Execute()
    {
        _console.WriteLine("Choose role:");
        _console.WriteLine("1. Become a client in bank");
        _console.WriteLine("2. Become a employee in bank");

        var choice = _console.ReadLine();
        NextViewName = choice switch
        {
            "1" => PageName.RegistrationClientPage,
            "2" => PageName.RegistrationEmployeePage,
            _ => PageName.ChooseRolePage
        };
    }
}