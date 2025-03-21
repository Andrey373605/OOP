using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.AdministratorLoanRequestPage)]
public class AdministratorLoanRequestView : IView
{
    private readonly IApplicationService _applicationService;
    private readonly ILoanService _loanService;
    private readonly IConsole _console;
    private readonly IInputHandler _input;

    public AdministratorLoanRequestView(IApplicationService applicationService, IConsole console, IInputHandler input, ILoanService loanService)
    {
        _applicationService = applicationService;
        _console = console;
        _input = input;
        _loanService = loanService;
    }
    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        _console.WriteLine("Loan applications");
        var loans = await _loanService.GetLoanApplications();
        foreach (var l in loans)
        {
            _console.WriteLine($"Id: {l.Id}" +
                               $"Client: {l.ClientId}\t" +
                               $"Amount: {l.Amount}\t" +
                               $"Duration: {l.NumberOfPayments} month");
        }
        
        _console.WriteLine("1. Approve loan");
        _console.WriteLine("2. Cancel loan");
        _console.WriteLine("3. Return back");

        var choice = _input.GetNumberVariant(3);
        if (choice == "1")
        {
            var id = _input.GetIntNumber("Enter Id loan: ", new IntValidator());
            _console.Clear();
            try
            {
                await _loanService.ApproveLoanRequest(id);
                _console.WriteLine("Loan application approved");
            }
            catch (Exception e)
            {
                _console.WriteLine(e.Message);
            }
        }
        else if (choice == "2")
        {
            var id = _input.GetIntNumber("Enter Id loan: ", new IntValidator());
            _console.Clear();
            try
            {
                await _loanService.RejectLoanRequest(id);
                _console.WriteLine("Loan application rejected");
            }
            catch (Exception e)
            {
                _console.WriteLine(e.Message);
            }
        }
        
        NextViewName = PageName.ManagerMainMenuPage;
    }
}