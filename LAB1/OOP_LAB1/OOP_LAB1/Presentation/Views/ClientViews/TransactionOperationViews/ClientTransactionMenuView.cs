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
        _console.WriteLine("1. All my transactions");
        _console.WriteLine("2. Deposit");
        _console.WriteLine("3. Withdraw");
        _console.WriteLine("4. Transfer");
        _console.WriteLine("5. Return back");

        var choice = _input.GetNumberVariant(5);

        _console.Clear();
        NextViewName = choice switch
        {
            "1" => PageName.ClientAllTransactionsPage,
            "2" => PageName.ClientDepositAccountPage,
            "3" => PageName.ClientWithdrawAccountPage,
            "4" => PageName.ClientTransferAccountPage,
            "5" => PageName.ClientMainMenuPage,
            _ => PageName.ClientTransactionMenuPage
        };
    }
}