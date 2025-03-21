using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.SpecialistSalaryRequestPage)]
public class SpecialistSalaryRequestView : IView
{
    private ISalaryProjectService _salaryProjectService;
    private IConsole _console;
    private IInputHandler _input;

    public SpecialistSalaryRequestView(ISalaryProjectService salaryProjectService, IConsole console, IInputHandler input)
    {
        _salaryProjectService = salaryProjectService;
        _console = console;
        _input = input;
    }

    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        _console.WriteLine("All salaries requests:");
        var requests = await _salaryProjectService.GetAllSalaryRequests();
        foreach (var r in requests)
        {
            _console.WriteLine($"Id: {r.Id}\t" +
                               $"Salary project Id: {r.SalaryProjectId}" +
                               $"Amount {r.Amount}");
        }
        
        var requestId = _input.GetIntNumber("Enter request ID: ", new IntValidator());

        try
        {
            await _salaryProjectService.ApproveSalaryApplication(requestId);
            _console.WriteLine("Salary application approved.");
        }
        catch (Exception e)
        {
            _console.WriteLine(e.Message);
        }
        
        NextViewName = PageName.SpecialistMainMenuPage;
    }
}