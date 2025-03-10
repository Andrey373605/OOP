using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;

namespace OOP_LAB1.Presentation.Views;

public class ExitView : IView
{
    IConsole _console;

    public ExitView(IConsole console)
    {
        _console = console;
    }
    public PageName? NextViewName { get; }
    public async Task Execute()
    {
        _console.WriteLine("Exit");
    }
}