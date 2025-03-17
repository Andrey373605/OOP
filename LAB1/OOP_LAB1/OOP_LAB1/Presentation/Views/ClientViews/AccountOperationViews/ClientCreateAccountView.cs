using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.ClientCreateAccountPage)]
public class ClientCreateAccountView : IView
{
    private readonly IConsole _console;
    private readonly IInputHandler _inputHandler;
    private readonly IApplicationService _applicationService;

    public ClientCreateAccountView(IConsole console, IInputHandler inputHandler , IApplicationService applicationService)
    {
        _console = console;
        _inputHandler = inputHandler;
        _applicationService = applicationService;
    }
    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        _console.WriteLine("1. Create acount");
        _console.WriteLine("2. Return back");
        var choice = _inputHandler.GetNumberVariant(2);
        
        if (choice == "1")
        {
            _console.Clear();
            try
            {
                await _applicationService.CreateAccount();
                _console.WriteLine("Account created successfully");
            }
            catch (Exception e)
            {
                System.Console.WriteLine(e);
            }
            
        }

        NextViewName = PageName.ClientMainMenuPage;

    }
}