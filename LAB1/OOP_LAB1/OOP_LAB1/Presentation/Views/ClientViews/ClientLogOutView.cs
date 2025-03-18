using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Navigator;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.ClientLogOutPage)]
public class ClientLogOutView : IView
{
    IConsole _console;
    IApplicationService _applicationService;

    public ClientLogOutView(IConsole console, IApplicationService applicationService)
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