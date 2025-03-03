using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;

namespace OOP_LAB1.Presentation.Views;

public class MainMenuView : IView
{
    public PageName? NextViewName { get; private set; }
    private readonly IConsoleView _console;

    public MainMenuView(IConsoleView console)
    {
        _console = console;
    }

    public void Execute()
    {
        _console.WriteLine("1. Choose bank");
        _console.WriteLine("2. Exit");
        
        var choice = _console.ReadLine();
        NextViewName = choice switch
        {
            "1" => PageName.BankChoosePage,
            _ => NextViewName
        };
    }
}