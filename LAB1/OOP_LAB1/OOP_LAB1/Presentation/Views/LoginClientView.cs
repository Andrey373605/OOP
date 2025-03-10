using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views;

public class LoginClientView : IView
{
    private readonly IConsole _console;
    private readonly IApplicationService _applicationService;

    public LoginClientView(IConsole console, IApplicationService applicationService)
    {
        _console = console;
        _applicationService = applicationService;
    }
    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        try
        {
            await _applicationService.LoginClient();
            _console.WriteLine($"Successfully logged in");
            NextViewName = PageName.ClientMainMenuPage;
        }
        catch (Exception e)
        {
            _console.WriteLine(e.Message);
            NextViewName = PageName.ChooseRolePage;
        }
        
        //успешный вход
        NextViewName = PageName.ClientMainMenuPage;
    }
}