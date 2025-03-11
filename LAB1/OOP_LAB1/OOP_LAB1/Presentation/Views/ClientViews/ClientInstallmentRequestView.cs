using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views.ClientViews;

public class ClientInstallmentRequestView : IView
{
    IConsole _console;
    IApplicationService _applicationService;
    IInputHandler _input;

    public ClientInstallmentRequestView(IConsole console, IApplicationService applicationService, IInputHandler inputHandler)
    {
        _console = console;
        _applicationService = applicationService;
        _input = inputHandler;
    }
    
    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        _console.WriteLine("1. Continue");
        _console.WriteLine("2. Return back");
        
        var choice = _console.ReadLine();
        if (choice == "1")
        {
            var sum = _input.GetDecimalNumber("Enter sum: ", new SumValidator());
            
            var duration = _input.GetIntNumber("Enter month count: ", new MonthValidator());
            
            try
            {
                await _applicationService.CreateInstallmentRequest(sum, duration);
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