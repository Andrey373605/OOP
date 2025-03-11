using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views.ClientViews;

public class ClientFreezeAccountView : IView
{
    private readonly IConsole _console;
    private readonly IAccountService _accountService;
    private readonly IApplicationService _applicationService;
    private readonly IInputHandler _input;

    public ClientFreezeAccountView(IConsole console, IAccountService accountService, IApplicationService applicationService, IInputHandler input)
    {
        _console = console;
        _accountService = accountService;
        _applicationService = applicationService;
        _input = input;
    }
    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        _console.WriteLine("1. Freeze account");
        _console.WriteLine("2. Return back");
        
        var choice = _console.ReadLine();
        if (choice == "1")
        {
            var accountId = _input.GetIntNumber("Account Id", new IntValidator());
            
            try
            {
                await _applicationService.FreezeAccount(accountId);
                _console.WriteLine("Account freeze successfully");
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }
            
        }

        NextViewName = PageName.ClientMainMenuPage;

    }
}