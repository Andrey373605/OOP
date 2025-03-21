using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.AdministratorSalaryProjectRequestPage)]
public class AdministratorSalaryProjectRequestView : IView
{
    private ISalaryProjectService _salaryProjectService;
    private IConsole _console;
    private IInputHandler _input;

    public AdministratorSalaryProjectRequestView(ISalaryProjectService salaryProjectService, IConsole console, IInputHandler input)
    {
        _salaryProjectService = salaryProjectService;
        _console = console;
        _input = input;
    }

    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        var requests = await _salaryProjectService.GetAllSalaryProjectRequests();
        foreach (var r in requests)
        {
            _console.WriteLine($"Salary project Id: {r.Id}\t" +
                               $"Enterprise Id: {r.EnterpriseId}\t" +
                               $"Bank id: {r.BankId}");
        }
        _console.WriteLine("1. Approve salary project");
        _console.WriteLine("2. reject salary project");
        _console.WriteLine("3. Return back");

        var choice = _input.GetNumberVariant(3);
        if (choice == "1")
        {
            var id = _input.GetIntNumber("Enter Id project: ", new IntValidator());
            _console.Clear();
            try
            {
                await _salaryProjectService.ApproveSalaryProjectApplication(id);
                _console.WriteLine("Salary project application approved");
            }
            catch (Exception e)
            {
                _console.WriteLine(e.Message);
            }
        }
        else if (choice == "2")
        {
            var id = _input.GetIntNumber("Enter Id salary project: ", new IntValidator());
            _console.Clear();
            try
            {
                await _salaryProjectService.RejectSalaryProjectApplication(id);
                _console.WriteLine("Salary project application rejected");
            }
            catch (Exception e)
            {
                _console.WriteLine(e.Message);
            }
        }

        NextViewName = PageName.AdministratorMainMenuPage;
    }
}