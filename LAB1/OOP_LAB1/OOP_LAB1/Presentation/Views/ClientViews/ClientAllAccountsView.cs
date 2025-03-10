using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;

namespace OOP_LAB1.Presentation.Views.ClientViews;

public class ClientAllAccountsView : IView
{
    private readonly IConsole _console;
    private readonly IAccountService _accountService;
    private readonly IContext _context;

    public ClientAllAccountsView(IConsole console, IAccountService accountService, IContext context)
    {
        _console = console;
        _accountService = accountService;
        _context = context;
    }
    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        _console.WriteLine("Accounts:");
        var accounts = await _accountService.GetAllClientAccountsAsync(_context);
        foreach (var a in accounts)
        {
            _console.WriteLine($"Id: {a.Id} \t Balance: {a.Balance}" );
        }

        NextViewName = PageName.ClientMainMenuPage;

    }
}