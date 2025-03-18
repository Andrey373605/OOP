using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.ClientTransactionMenuPage)]
public class ClientTransactionMenuView : IView
{
    IConsole _console;
    IInputHandler _input;
    IApplicationService _applicationService;

    public ClientTransactionMenuView(IConsole console, IInputHandler inputHandler, IApplicationService applicationService)
    {
        _console = console;
        _input = inputHandler;
        _applicationService = applicationService;
    }
    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        _console.WriteLine("1. All my transfers");
        _console.WriteLine("2. All my deposits");
        _console.WriteLine("3. All my withdraws");
        _console.WriteLine("4. Deposit");
        _console.WriteLine("5. Withdraw");
        _console.WriteLine("6. Transfer");
        _console.WriteLine("7. Return back");

        var choice = _input.GetNumberVariant(7);

        _console.Clear();
        NextViewName = choice switch
        {
            "1" => PageName.ClientAllTransfersPage,
            "2" => PageName.ClientAllDepositsPage,
            "3" => PageName.ClientAllWithdrawsPage,
            "4" => PageName.ClientDepositAccountPage,
            "5" => PageName.ClientWithdrawAccountPage,
            "6" => PageName.ClientTransferAccountPage,
            "7" => PageName.ClientMainMenuPage,
            _ => PageName.ClientTransactionMenuPage
        };
    }
}