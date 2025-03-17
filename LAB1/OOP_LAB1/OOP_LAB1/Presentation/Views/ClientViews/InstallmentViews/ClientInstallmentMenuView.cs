using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.ClientInstallmentMenuPage)]
public class ClientInstallmentMenuView : IView
{
    IConsole _console;
    IInputHandler _input;
    IApplicationService _applicationService;

    public ClientInstallmentMenuView(IConsole console, IInputHandler inputHandler, IApplicationService applicationService)
    {
        _console = console;
        _input = inputHandler;
        _applicationService = applicationService;
    }
    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        _console.WriteLine("1. All my installments");
        _console.WriteLine("2. Installment request");
        _console.WriteLine("3. Return back");

        var choice = _input.GetNumberVariant(3);

        _console.Clear();
        NextViewName = choice switch
        {
            "1" => PageName.ClientAllInstallmentPage,
            "2" => PageName.ClientInstallmentRequestPage,
            "3" => PageName.ClientMainMenuPage,
            _ => PageName.ClientInstallmentMenuPage,
        };
    }
}