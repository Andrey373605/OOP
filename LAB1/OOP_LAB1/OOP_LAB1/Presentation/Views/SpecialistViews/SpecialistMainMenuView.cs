using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.SpecialistMainMenuPage)]
public class SpecialistMainMenuView : IView
{
    public PageName? NextViewName { get; private set; }
    
    private readonly IInputHandler _input;
    private readonly IAuthorizationService _auth;
    private readonly IConsole _console;
    private readonly IApplicationService _applicationService;

    public SpecialistMainMenuView(IInputHandler input, IAuthorizationService auth, IConsole console, IApplicationService applicationService)
    {
        _input = input;
        _auth = auth;
        _console = console;
        _applicationService = applicationService;
    }
    public async Task Execute()
    {
        _console.WriteLine("1. Salary project application");
        _console.WriteLine("2. Salary request");
        _console.WriteLine("3. Deposit salary project");
        _console.WriteLine("4. Pay salary project");
        _console.WriteLine("5. Log out");
        _console.WriteLine("6. Exit");
        
        var choice = _input.GetNumberVariant(6);
        _console.Clear();
        NextViewName = choice switch
        {
            "1" => PageName.SpecialistProjectApplicationPage,
            "2" => PageName.SpecialistSalaryRequestPage,
            "3" => PageName.SpeccialistDepositSalaryProjectPage,
            "4" => PageName.SpecialistPaySalaryPage,
            "5" => PageName.LogOutPage,
            "6" => PageName.ExitPage,
            _ => NextViewName
        };
    }
}