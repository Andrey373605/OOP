using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;

namespace OOP_LAB1.Presentation.Views.ClientViews;

public class ClientCreateAccountView : IView
{
    private readonly IConsole _console;
    private readonly IAccountService _accountService;
    private readonly IApplicationService _applicationService;

    public ClientCreateAccountView(IConsole console, IAccountService accountService, IApplicationService applicationService)
    {
        _console = console;
        _accountService = accountService;
        _applicationService = applicationService;
    }
    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        _console.WriteLine("Create acount");
        

    }
}