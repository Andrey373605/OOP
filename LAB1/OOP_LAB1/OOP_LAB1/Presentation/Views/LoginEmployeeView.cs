using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;

namespace OOP_LAB1.Presentation.Views;

public class LoginEmployeeView : IView
{
    IApplicationService _applicationService;
    IConsole _console;

    public LoginEmployeeView(IApplicationService applicationService, IConsole console)
    {
        _applicationService = applicationService;
        _console = console;
    }
    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        try
        {
            await _applicationService.LoginEmployee();
            var role = await _applicationService.GetCurrentEmployeeRole();

            NextViewName = role switch
            {
                EmployeeRole.Administrator => PageName.AdministratorMainMenuPage,
                EmployeeRole.Manager => PageName.ManagerMainMenuPage,
                EmployeeRole.Operator => PageName.OperatorMainMenuPage,
                EmployeeRole.ExternalSpecialist => PageName.ExternalSpecialistMainMenuPage,
                _ => PageName.ChooseRolePage
            };
            _console.WriteLine($"Successfully login with Role: {role.ToString()}");
        }
        catch (Exception e)
        {
            System.Console.WriteLine(e);
            throw;
        }
    }
    
}