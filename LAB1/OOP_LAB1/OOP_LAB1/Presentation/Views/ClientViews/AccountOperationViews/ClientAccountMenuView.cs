using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.ClientAccountMenuPage)]
public class ClientAccountMenuView
{
    IConsole _console;
    IInputHandler _input;
    IApplicationService _applicationService;

    public ClientAccountMenuView(IConsole console, IInputHandler inputHandler, IApplicationService applicationService)
    {
        _console = console;
        _input = inputHandler;
        _applicationService = applicationService;
    }
    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        _console.WriteLine("1. All my accounts");
        _console.WriteLine("2. Create Account");
        _console.WriteLine("3. Freeze Account");
        _console.WriteLine("4. Unfreeze Account");
        _console.WriteLine("5. Return back");

        var choice = _input.GetNumberVariant(5);

        _console.Clear();
        NextViewName = choice switch
        {
            "1" => PageName.ClientAllAccountsPage,
            "2" => PageName.ClientCreateAccountPage,
            "3" => PageName.ClientFreezeAccountPage,
            "4" => PageName.ClientUnfreezeAccountPage,
            "5" => PageName.ClientMainMenuPage,
            _ => PageName.ClientAccountMenuPage,
        };
    }
}