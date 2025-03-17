using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.MainMenuPage)]
public class MainMenuView : IView
{
    public PageName? NextViewName { get; private set; }
    private readonly IConsole _console;
    private readonly IInputHandler _inputHandler;

    public MainMenuView(IConsole console, IInputHandler inputHandler)
    {
        _console = console;
        _inputHandler = inputHandler;
    }

    public async Task Execute()
    {
        _console.WriteLine("1. Registration");
        _console.WriteLine("2. Login");
        _console.WriteLine("3. Exit");

        
        var choice = _inputHandler.GetNumberVariant(3);
        _console.Clear();
        NextViewName = choice switch
        {
            "1" => PageName.RegistrationUserPage,
            "2" => PageName.LoginUserPage,
            _ => PageName.ExitPage
        };
    }
}