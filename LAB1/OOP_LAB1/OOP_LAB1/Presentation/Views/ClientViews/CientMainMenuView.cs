using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;

namespace OOP_LAB1.Presentation.Views.ClientViews;

public class ClientMainMenuView : IView
{
    public PageName? NextViewName { get; private set; }
    
    private readonly IInputHandler _input;
    private readonly IAuthorizationService _auth;
    private readonly IConsole _console;
    private readonly IContext _context;

    public ClientMainMenuView(IInputHandler input, IAuthorizationService auth, IConsole console, IContext context)
    {
        _input = input;
        _auth = auth;
        _console = console;
        _context = context;
    }
    public async Task Execute()
    {
        _console.WriteLine("1. Show all accounts");
        
        var choice = _console.ReadLine();
        NextViewName = choice switch
        {
            "1" => PageName.ClientAllAccountsPage,
            "2" => PageName.LoginClientPage,
            "3" => PageName.RegistrationEmployeePage,
            "4" => PageName.LoginEmployeePage,
            "5" => PageName.ExitPage,
            _ => NextViewName
        };
    }
}