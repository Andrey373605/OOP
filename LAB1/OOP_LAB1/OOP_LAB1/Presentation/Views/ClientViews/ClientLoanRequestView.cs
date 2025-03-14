using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views.ClientViews;

public class ClientLoanRequestView : IView
{
    IConsole _console;
    IApplicationService _applicationService;
    IInputHandler _input;

    public ClientLoanRequestView(IConsole console, IApplicationService applicationService, IInputHandler inputHandler)
    {
        _console = console;
        _applicationService = applicationService;
        _input = inputHandler;
    }
    
    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        _console.WriteLine("1. Continue account");
        _console.WriteLine("2. Return back");

        var choice = _input.GetNumberVariant(2);
        if (choice == "1")
        {
            var sum = _input.GetDecimalNumber("Enter sum:", new SumValidator());
            
            _console.WriteLine("1. 3 Month");
            _console.WriteLine("2. 6 Month");
            _console.WriteLine("3. 12 Month");
            _console.WriteLine("4. 24 Month");

            var duration = _input.GetIntNumber("Enter month count:", new MonthValidator());
            
            var rate = _input.GetIntNumber("Enter rate:", new RateValidator());
            
            try
            {
                await _applicationService.CreateLoanRequest(sum, rate, duration);
                _console.WriteLine("Request created successfully");
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }
            
        }
        
        NextViewName = PageName.ClientMainMenuPage;
        
    }
}