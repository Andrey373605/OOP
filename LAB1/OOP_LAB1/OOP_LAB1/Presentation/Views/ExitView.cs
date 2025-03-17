using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Navigator;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.ExitPage)]
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
        _console.Clear();
        _console.WriteLine("Exit");
    }
}