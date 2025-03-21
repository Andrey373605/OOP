using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.AdministratorInstallmentRequestPage)]
public class AdministratorInstallmentRequestView : IView
{
    private readonly IApplicationService _applicationService;
    private readonly IConsole _console;
    private readonly IInputHandler _input;
    private readonly IInstallmentService _installmentService;

    public AdministratorInstallmentRequestView(IApplicationService applicationService, IConsole console, IInputHandler input, IInstallmentService installmentService)
    {
        _applicationService = applicationService;
        _console = console;
        _input = input;
        _installmentService = installmentService;
    }
    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        _console.WriteLine("Installment applications");
        var installments = await _installmentService.GetInstallmentApplications();
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
                await _installmentService.ApproveInstallmentRequest(id);
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
                await _installmentService.RejectInstallmentRequest(id);
                _console.WriteLine("Installment application rejected");
            }
            catch (Exception e)
            {
                _console.WriteLine(e.Message);
            }
        }
        
        NextViewName = PageName.AdministratorMainMenuPage;
    }
}