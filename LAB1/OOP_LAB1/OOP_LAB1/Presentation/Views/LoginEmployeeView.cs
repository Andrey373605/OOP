using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Navigator;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.LoginEmployeePage)]
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
        _console.Clear();
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
            NextViewName = PageName.OperatorMainMenuPage;
        }
        catch (Exception e)
        {
            _console.WriteLine(e.Message);
            NextViewName = PageName.ChooseRolePage;
        }
    }
    
}