using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.SpecialistProjectApplicationPage)]
public class SpecialistProjectApplicationView :IView
{
    IEnterpriseService _enterpriseService;
    private readonly ISalaryProjectService _salaryProjectService;
    private readonly IConsole _console;
    private IInputHandler _input;

    public SpecialistProjectApplicationView(IConsole console, IEnterpriseService enterpriseService, IInputHandler input, ISalaryProjectService salaryProjectService)
    {
        _console = console;
        _enterpriseService = enterpriseService;
        _input = input;
        _salaryProjectService = salaryProjectService;
    }


    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        
        _console.WriteLine("Choose enterprise");
        var enterprises = await _enterpriseService.GetAllEnterprises();
        foreach (var e in enterprises)
        {
            _console.WriteLine($"Name: {e.LegalName}\t" +
                               $"Id: {e.Id}");
        }
        var enterpriseId = _input.GetIntNumber("Enter enterprise id:", new IntValidator());

        _console.Clear();
        try
        {
            await _salaryProjectService.CreateSalaryProjectApplication(enterpriseId);
            _console.WriteLine("Salary project application created successfully.");
        }
        catch (Exception e)
        {
            _console.WriteLine(e.Message);
        }
        
        NextViewName = PageName.SpecialistMainMenuPage;
    }
}