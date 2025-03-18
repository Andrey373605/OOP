using System.ComponentModel;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.ClientAllTransfersPage)]
public class ClientAllTransfersView : IView
{
    IConsole _console;
    IApplicationService _applicationService;
    private IInputHandler _input;

    public ClientAllTransfersView(IConsole console, IApplicationService applicationService, IInputHandler input)
    {
        _console = console;
        _applicationService = applicationService;
        _input = input;
    }
    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        _console.WriteLine("Accounts: ");
        try
        {
            var accounts = await _applicationService.GetCurrentClientAccounts();
            if (accounts.Any())
            {
                foreach (var a in accounts)
                {

                    _console.WriteLine($"Id: {a.Id} \t Balance: {a.Balance} \t Active: {a.Status.ToString()}" +
                                       $" \t Type: {a.AccountType.ToString()}" );
                }
            }
        }
        catch (Exception e)
        {
            _console.WriteLine(e.Message);
        }
        
        var accountId = _input.GetIntNumber("Enter account number: ", new IntValidator());
        _console.Clear();
        try
        {
            var transfers = await _applicationService.GetTransfersByAccountIdAsync(accountId);
            _console.WriteLine("Transfers: ");
            foreach (var t in transfers)
            {
                var type = accountId == t.FromAccountId ? "sending" : "receiving";
                _console.WriteLine($"From: {t.FromAccountId} \t" +
                                   $"To: {t.ToAccountId}\t" +
                                   $"Amount: {t.Amount}\t" +
                                   $"Date: {t.Date.Date}\t" +
                                   $"Type: {type}");
            }

            NextViewName = PageName.ClientTransactionMenuPage;
        }
        catch (Exception e)
        {
            _console.WriteLine(e.Message);
        }
    }
}