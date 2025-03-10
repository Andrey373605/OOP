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
    
    private readonly IInputHandler _input;
    private readonly IAuthorizationService _auth;
    private readonly IConsole _console;
    private readonly IContext _context;
    private readonly IBankService _bankService;

    public ChooseRoleView(IInputHandler input, IAuthorizationService auth, IConsole console, IContext context, IBankService bankService)
    {
        _input = input;
        _auth = auth;
        _console = console;
        _context = context;
        _bankService = bankService;
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
            _ => NextViewName
        };

        
        


    }
}