using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views;

public class LoginClientView : IView
{
    private readonly IInputHandler _input;
    private readonly IAuthorizationService _auth;
    private readonly IConsole _console;

    public LoginClientView(IInputHandler input, IAuthorizationService auth, IConsole console)
    {
        _input = input;
        _auth = auth;
        _console = console;
    }
    public PageName? NextViewName { get; private set; }
    public void Execute()
    {
        try
        {
            _auth.AuthenticateClientAsync().GetAwaiter().GetResult();
            _console.WriteLine($"Successfully logged in");
            NextViewName = PageName.ChooseBankPage;
        }
        catch (Exception e)
        {
            _console.WriteLine(e.Message);
            NextViewName = PageName.ChooseRolePage;
        }
        
        
    }
}