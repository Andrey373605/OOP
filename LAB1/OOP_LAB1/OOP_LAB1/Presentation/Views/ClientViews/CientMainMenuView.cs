using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;

namespace OOP_LAB1.Presentation.Views.ClientViews;

public class ClientMainMenuView : IView
{
    public PageName? NextViewName { get; }
    
    private readonly IInputHandler _input;
    private readonly IAuthorizationService _auth;
    private readonly IConsoleView _console;
    private readonly IContext _context;

    public ClientMainMenuView(IInputHandler input, IAuthorizationService auth, IConsoleView console, IContext context)
    {
        _input = input;
        _auth = auth;
        _console = console;
        _context = context;
    }
    public void Execute()
    {
        _console.WriteLine("1. Show all accounts");
        _console.WriteLine("2. Show all customers");
    }
}