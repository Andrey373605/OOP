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
        _console.WriteLine("1. View logs");
        _console.WriteLine("2. Cancel transfers");
        _console.WriteLine("3. Employee registration requests");
        _console.WriteLine("4. Client registration requests");
        _console.WriteLine("5. Loan requests");
        _console.WriteLine("6. Installment requests");
        _console.WriteLine("7. Salary project requests");
        _console.WriteLine("8. Log out");
        _console.WriteLine("9. Exit");

        var choice = _input.GetNumberVariant(9);
        _console.Clear();
        NextViewName = choice switch
            
        {
            "1" => PageName.AdministratorLogsPage,
            "2" => PageName.AdministratorCancelTransferPage,
            "3" => PageName.AdministratorEmployeeRegistrationRequestPage,
            "4" => PageName.AdministratorClientRegistrationRequestPage,
            "5" => PageName.AdministratorLoanRequestPage,
            "6" => PageName.AdministratorInstallmentRequestPage,
            "7" => PageName.AdministratorSalaryProjectRequestPage,
            "8" => PageName.LogOutPage,
            "9" => PageName.ExitPage,
            _ => PageName.AdministratorMainMenuPage
        };
    }
}