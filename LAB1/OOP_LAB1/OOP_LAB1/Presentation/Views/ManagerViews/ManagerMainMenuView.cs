using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.ManagerMainMenuPage)]
public class ManagerMainMenuView : IView
{
    public PageName? NextViewName { get; private set; }
    
    private readonly IInputHandler _input;
    private readonly IAuthorizationService _auth;
    private readonly IConsole _console;
    private readonly IApplicationService _applicationService;

    public ManagerMainMenuView(IInputHandler input, IAuthorizationService auth, IConsole console, IApplicationService applicationService)
    {
        _input = input;
        _auth = auth;
        _console = console;
        _applicationService = applicationService;
    }
    public async Task Execute()
    {
        _console.WriteLine("1. View account transfers");
        _console.WriteLine("2. View account deposits");
        _console.WriteLine("3. View account withdrawals");
        _console.WriteLine("4. Cancel transfer");
        _console.WriteLine("5. Approve salary project");
        _console.WriteLine("6. Log out");
        
        var choice = _input.GetNumberVariant(6);
        _console.Clear();
        NextViewName = choice switch
        {
            "1" => PageName.ManagerAllTransfersPage,
            "2" => PageName.ManagerAllDepositsPage,
            "3" => PageName.ManagerAllWithdrawsPage,
            "4" => PageName.ManagerCancelTransferPage,
            "5" => PageName.ManagerApproveSalaryProjectPage,
            "6" => PageName.LogOutPage,
            _ => NextViewName
        };
    }
}