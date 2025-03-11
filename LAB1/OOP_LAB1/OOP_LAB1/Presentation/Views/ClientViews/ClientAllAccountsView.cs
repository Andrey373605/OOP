using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;

namespace OOP_LAB1.Presentation.Views.ClientViews;

public class ClientAllAccountsView : IView
{
    private readonly IConsole _console;
    private readonly IAccountService _accountService;
    private readonly IApplicationService _applicationService;

    public ClientAllAccountsView(IConsole console, IAccountService accountService, IApplicationService applicationService)
    {
        _console = console;
        _accountService = accountService;
        _applicationService = applicationService;
    }
    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        _console.WriteLine("Accounts:");
        var accounts = await _applicationService.GetCurrentClientAccounts();
        foreach (var a in accounts)
        {
            _console.WriteLine(a.AccountType.ToString());
            var type = a.AccountType switch
            {
                AccountType.Loan => "loan",
                AccountType.Installment => "installment",
                AccountType.Saving => "saving",
                _ => "error"
            };
            _console.WriteLine($"Id: {a.Id} \t Balance: {a.Balance} \t Active: {!a.IsBlocked} \t Type: {type}" );
        }

        NextViewName = PageName.ClientMainMenuPage;

    }
}