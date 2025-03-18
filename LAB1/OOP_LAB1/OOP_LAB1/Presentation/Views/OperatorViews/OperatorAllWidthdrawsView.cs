using System.ComponentModel;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.OperatorAllWithdrawsPage)]
public class OperatorAllWithdrawsView : IView
{
    IConsole _console;
    IApplicationService _applicationService;
    private IInputHandler _input;

    public OperatorAllWithdrawsView(IConsole console, IApplicationService applicationService, IInputHandler input)
    {
        _console = console;
        _applicationService = applicationService;
        _input = input;
    }
    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        
        var accountId = _input.GetIntNumber("Enter account number: ", new IntValidator());
        _console.Clear();
        try
        {
            var transfers = await _applicationService.GetWithdrawsByAccountIdAsync(accountId);
            _console.WriteLine("Withdraws by account: ");
            foreach (var t in transfers)
            {
                _console.WriteLine($"From: {t.FromAccountId}\t" +
                                   $"Amount: {t.Amount}\t" +
                                   $"Date: {t.Date}");
            }
        }
        catch (Exception e)
        {
            _console.WriteLine(e.Message);
        }
        NextViewName = PageName.OperatorMainMenuPage;
    }
}