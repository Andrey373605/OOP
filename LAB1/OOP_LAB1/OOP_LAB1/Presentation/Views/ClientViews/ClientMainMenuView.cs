using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.ClientMainMenuPage)]
public class ClientMainMenuView : IView
{
    public PageName? NextViewName { get; private set; }
    
    private readonly IInputHandler _input;
    private readonly IAuthorizationService _auth;
    private readonly IConsole _console;
    private readonly IApplicationService _applicationService;

    public ClientMainMenuView(IInputHandler input, IAuthorizationService auth, IConsole console, IApplicationService applicationService)
    {
        _input = input;
        _auth = auth;
        _console = console;
        _applicationService = applicationService;
    }
    public async Task Execute()
    {
        _console.WriteLine("1. Account manage");
        _console.WriteLine("2. Loan manage");
        _console.WriteLine("3. Installment manage");
        _console.WriteLine("4. Transfer manage");
        _console.WriteLine("5. Log out");
        
        var choice = _input.GetNumberVariant(5);
        _console.Clear();
        NextViewName = choice switch
        {
            "1" => PageName.ClientAccountMenuPage,
            "2" => PageName.ClientLoanMenuPage,
            "3" => PageName.ClientInstallmentMenuPage,
            "4" => PageName.ClientTransactionMenuPage,
            "5" => PageName.LogOutPage,
            _ => NextViewName
        };
    }
}