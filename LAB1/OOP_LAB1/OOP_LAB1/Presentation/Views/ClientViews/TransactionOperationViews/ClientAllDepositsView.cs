using System.ComponentModel;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.ClientAllDepositsPage)]
public class ClientAllDepositsView : IView
{
    IConsole _console;
    IApplicationService _applicationService;
    private IInputHandler _input;

    public ClientAllDepositsView(IConsole console, IApplicationService applicationService, IInputHandler input)
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
            var transfers = await _applicationService.GetDepositsByAccountIdAsync(accountId);
            _console.WriteLine("Deposit by account: ");
            foreach (var t in transfers)
            {
                _console.WriteLine($"To: {t.ToAccountId}\t" +
                                   $"Amount: {t.Amount}\t" +
                                   $"Date: {t.Date}");
            }

            
        }
        catch (Exception e)
        {
            _console.WriteLine(e.Message);
        }
        NextViewName = PageName.ClientTransactionMenuPage;
    }
}