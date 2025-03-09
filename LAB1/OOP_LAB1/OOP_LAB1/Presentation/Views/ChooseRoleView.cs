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
    public void Execute()
    {
        _console.WriteLine("Choose:");
        _console.WriteLine("1. Become a client in bank");
        _console.WriteLine("2. Become a employee in bank");
        _console.WriteLine("3. Log in");

        var choice = _console.ReadLine();
        
        


    }
}