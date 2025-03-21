using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.SpeccialistDepositSalaryProjectPage)]
public class SpecialistDepositSalaryProjectView : IView
{
    private ISalaryProjectService _salaryProjectService;
    private IConsole _console;
    private IInputHandler _input;

    public SpecialistDepositSalaryProjectView(ISalaryProjectService salaryProjectService, IConsole console, IInputHandler input)
    {
        _salaryProjectService = salaryProjectService;
        _console = console;
        _input = input;
    }

    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        _console.WriteLine("Choose project that you want to deposit");
        var salaryProjects = await _salaryProjectService.GetAllSalaryProjects();
        foreach (var s in salaryProjects)
        {
            _console.WriteLine($"Id: {s.Id}\t" +
                               $"Enterprise Id: {s.EnterpriseId}");
        }
        
        var id = _input.GetIntNumber("Enter id of the salary project you want to deposit", new IntValidator());
        var amount = _input.GetDecimalNumber("Enter amount of salary project you want to deposit", new SumValidator());
        try
        {
            await _salaryProjectService.DepositProjectAccount(id, amount);
            _console.WriteLine($"Salary project {id} has been deposited");
        }
        catch (Exception e)
        {
            _console.WriteLine(e.Message);
        }

        NextViewName = PageName.SpecialistMainMenuPage;
    }
}