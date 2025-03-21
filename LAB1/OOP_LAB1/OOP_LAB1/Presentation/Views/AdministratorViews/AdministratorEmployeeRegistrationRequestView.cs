using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.AdministratorEmployeeRegistrationRequestPage)]
public class AdministratorEmployeeRegistrationRequestView : IView
{
    private readonly IApplicationService _applicationService;
    private readonly IConsole _console;
    private readonly IInputHandler _input;
    private readonly IEmployeeService _employeeService;

    public AdministratorEmployeeRegistrationRequestView(IApplicationService applicationService, IConsole console, IInputHandler input, IEmployeeService employeeService)
    {
        _applicationService = applicationService;
        _console = console;
        _input = input;
        _employeeService = employeeService;
    }
    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        _console.WriteLine("Registration employee requests");
        var employees = await _employeeService.GetClientRegistrationRequests();
        foreach (var e in employees)
        {
            _console.WriteLine($"Id: {e.Id}\t" +
                               $"Role: {e.Role}");
        }
        
        _console.WriteLine("1. Approve registration");
        _console.WriteLine("2. Reject registration");
        _console.WriteLine("3. Return back");

        var choice = _input.GetNumberVariant(3);
        _console.Clear();
        if (choice == "1")
        {
            var id = _input.GetIntNumber("Enter Id employee: ", new IntValidator());
            _console.Clear();
            try
            {
                await _employeeService.ApproveClientRegistration(id);
                _console.WriteLine("Registration employee approved");
            }
            catch (Exception e)
            {
                _console.WriteLine(e.Message);
            }
        }
        else if (choice == "2")
        {
            var id = _input.GetIntNumber("Enter Id employee: ", new IntValidator());
            _console.Clear();
            try
            {
                await _employeeService.RejectClientRegistration(id);
                _console.WriteLine("Employee registration rejected");
            }
            catch (Exception e)
            {
                _console.WriteLine(e.Message);
            }
        }
        
        NextViewName = PageName.AdministratorMainMenuPage;
    }
}