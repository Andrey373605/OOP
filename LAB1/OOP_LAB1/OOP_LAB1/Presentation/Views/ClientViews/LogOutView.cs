using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Navigator;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.LogOutPage)]
public class LogOutView : IView
{
    IConsole _console;
    IApplicationService _applicationService;

    public LogOutView(IConsole console, IApplicationService applicationService)
    {
        _console = console;
        _applicationService = applicationService;
    }
    
    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        _console.WriteLine("Log out");
        await _applicationService.LogOutUser();
        _console.Clear();
        NextViewName = PageName.MainMenuPage;
    }
}