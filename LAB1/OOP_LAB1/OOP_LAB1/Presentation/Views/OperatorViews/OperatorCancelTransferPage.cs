using System.ComponentModel;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.OperatorCancelTransferPage)]
public class OperatorCancelTransferPage : IView
{
    private IConsole _console;
    private IApplicationService _applicationService;
    private IInputHandler _input;

    public OperatorCancelTransferPage(IConsole console, IApplicationService applicationService, IInputHandler input)
    {
        _console = console;
        _applicationService = applicationService;
        _input = input;
    }
    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        var numberTransfer = _input.GetIntNumber("Enter transfer number:", new IntValidator());
        try
        {
            await _applicationService.CancelTransfer(numberTransfer);
            _console.WriteLine("Transfer has been cancelled.");
        }
        catch (Exception e)
        {
            _console.WriteLine(e.Message);
        }
        
        NextViewName = PageName.OperatorCancelTransferPage;
        
    }
}