using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views.SalaryProjectViews;

[ViewMapping(PageName.ClientSalaryRequestPage)]
public class ClientSalaryRequestView : IView
{
    private ISalaryProjectService _salaryProjectService;
    private IApplicationService _applicationService;
    private IConsole _console;
    private IInputHandler _input;

    public ClientSalaryRequestView(ISalaryProjectService salaryProjectService, IConsole console, IInputHandler input, IApplicationService applicationService)
    {
        _salaryProjectService = salaryProjectService;
        _console = console;
        _input = input;
        _applicationService = applicationService;
    }

    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        _console.WriteLine("Choose salary project:");
        var projects = await _salaryProjectService.GetAllSalaryProjects();
        foreach (var p in projects)
        {
            _console.WriteLine($"Id: {p.Id}");
        }
        var projectId = _input.GetIntNumber("Enter id of project:", new IntValidator());
        var amount = _input.GetDecimalNumber("Enter amount of salary:", new SumValidator());

        _console.Clear();
        try
        {
            await _applicationService.AddSalaryRequest(projectId, amount);
            _console.WriteLine("Salary Request Added");
        }
        catch (Exception e)
        {
            _console.WriteLine(e.Message);
            NextViewName = PageName.ClientSalaryRequestPage;
        }
        NextViewName = PageName.ClientMainMenuPage;
    }
}