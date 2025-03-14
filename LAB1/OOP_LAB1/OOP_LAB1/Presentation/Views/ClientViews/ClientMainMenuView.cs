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
        _console.WriteLine("3. Deposit account");
        _console.WriteLine("4. Transfer money");
        _console.WriteLine("5. Freeze account");
        _console.WriteLine("6. Unfreeze account");
        _console.WriteLine("7. Loan request");
        _console.WriteLine("8. Installment request");

        var choice = _input.GetNumberVariant(8);
        NextViewName = choice switch
        {
            "1" => PageName.ClientAllAccountsPage,
            "2" => PageName.ClientCreateAccountPage,
            "3" => PageName.ClientDepositAccountPage,
            "4" => PageName.ClientTransferAccountPage,
            "5" => PageName.ClientFreezeAccountPage,
            "6" => PageName.ClientUnfreezeAccountPage,
            "7" => PageName.ClientLoanRequestPage,
            "8" => PageName.CleintInstallmentRequstPage,
            _ => NextViewName
        };
    }
}