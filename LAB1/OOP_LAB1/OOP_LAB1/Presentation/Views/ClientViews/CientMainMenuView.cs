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
    private readonly IApplicationService _applicationService;

    public ClientMainMenuView(IInputHandler input, IAuthorizationService auth, IConsole console, IApplicationService applicationService)
    {
        _input = input;
        _auth = auth;
        _console = console;
        _applicationService = applicationService;
    }
    public async Task Execute()
    {
        _console.WriteLine("1. Show all accounts");
        _console.WriteLine("2. Create a new account");
        _console.WriteLine("3. Deposit Account");
        
        var choice = _console.ReadLine();
        NextViewName = choice switch
        {
            "1" => PageName.ClientAllAccountsPage,
            "2" => PageName.ClientCreateAccountPage,
            "3" => PageName.ClientDepositAccountPage,
            "4" => PageName.LoginEmployeePage,
            "5" => PageName.ExitPage,
            _ => NextViewName
        };
    }
}