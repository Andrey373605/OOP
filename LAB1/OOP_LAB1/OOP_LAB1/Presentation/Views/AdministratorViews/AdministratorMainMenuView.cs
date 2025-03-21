using System.Windows.Input;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;

namespace OOP_LAB1.Presentation.Views.AdministratorViews;

[ViewMapping(PageName.AdministratorMainMenuPage)]
public class AdministratorMainMenuView : IView
{
    private readonly IConsole _console;
    private readonly IApplicationService _applicationService;
    private readonly IInputHandler _input;

    public AdministratorMainMenuView(IConsole console, IApplicationService applicationService, IInputHandler inputHandler)
    {
        _console = console;
        _applicationService = applicationService;
        _input = inputHandler;
    }

    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        _console.WriteLine("1. View all logs");
        _console.WriteLine("2. Cancel transfers");
        _console.WriteLine("3. Employee registration requests");
        _console.WriteLine("4. Log out");

        var choice = _input.GetNumberVariant(4);
        _console.Clear();
        NextViewName = choice switch
            
        {
            "1" => PageName.AdministratorAllLogsPage,
            "2" => PageName.AdministratorCancelTransferPage,
            "3" => PageName.AdministratorEmployeeRegistrationRequestPage,
            "4" => PageName.LogOutPage,
            _ => PageName.AdministratorMainMenuPage
        };
    }
}