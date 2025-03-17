using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.ClientUnfreezeAccountPage)]
public class ClientUnfreezeAccountView : IView
{
    private readonly IConsole _console;
    private readonly IAccountService _accountService;
    private readonly IApplicationService _applicationService;
    private readonly IInputHandler _input;

    public ClientUnfreezeAccountView(IConsole console, IAccountService accountService, IApplicationService applicationService, IInputHandler input)
    {
        _console = console;
        _accountService = accountService;
        _applicationService = applicationService;
        _input = input;
    }
    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        _console.WriteLine("1. Unfreeze account");
        _console.WriteLine("2. Return back");

        var choice = _input.GetNumberVariant(2);
        if (choice == "1")
        {
            var accountId = _input.GetIntNumber("Account Id", new IntValidator());
            
            _console.Clear();
            try
            {
                await _applicationService.UnfreezeAccount(accountId);
                _console.WriteLine("Account unfreeze successfully");
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }
            
        }

        NextViewName = PageName.ClientAccountMenuPage;

    }
}