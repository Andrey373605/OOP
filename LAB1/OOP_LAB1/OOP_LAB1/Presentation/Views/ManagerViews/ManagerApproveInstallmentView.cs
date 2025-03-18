using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.ManagerApproveInstallmentPage)]
public class ManagerApproveInstallmentView : IView
{
    private readonly IApplicationService _applicationService;
    private readonly IConsole _console;
    private readonly IInputHandler _input;

    public ManagerApproveInstallmentView(IApplicationService applicationService, IConsole console, IInputHandler input)
    {
        _applicationService = applicationService;
        _console = console;
        _input = input;
    }
    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        _console.WriteLine("Installment applications");
        var installments = await _applicationService.GetInstallmentApplications();
        foreach (var i in installments)
        {
            _console.WriteLine($"Id: {i.Id}" +
                               $"Client: {i.ClientId}\t" +
                               $"Amount: {i.Amount}\t" +
                               $"Duration: {i.NumberOfPayments} month");
        }
        
        _console.WriteLine("1. Approve installment");
        _console.WriteLine("2. Cancel installment");
        _console.WriteLine("3. Return back");

        var choice = _input.GetNumberVariant(3);
        if (choice == "1")
        {
            var id = _input.GetIntNumber("Enter Id installment: ", new IntValidator());
            _console.Clear();
            try
            {
                await _applicationService.ApproveInstallmentByIdAsync(id);
                _console.WriteLine("Installment application approved");
            }
            catch (Exception e)
            {
                _console.WriteLine(e.Message);
            }
        }
        else if (choice == "2")
        {
            var id = _input.GetIntNumber("Enter Id installment: ", new IntValidator());
            _console.Clear();
            try
            {
                await _applicationService.RejectInstallmentByIdAsync(id);
                _console.WriteLine("Installment application rejected");
            }
            catch (Exception e)
            {
                _console.WriteLine(e.Message);
            }
        }
        
        NextViewName = PageName.ManagerMainMenuPage;
    }
}