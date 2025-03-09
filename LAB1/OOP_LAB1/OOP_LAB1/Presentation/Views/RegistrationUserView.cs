using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views;

public class RegistrationUserView : IView
{
    public PageName? NextViewName { get; private set; }
    
    private readonly IInputHandler _input;
    private readonly IAuthorizationService _auth;
    private readonly IConsole _console;
    private readonly IContext _context;

    public RegistrationUserView(IInputHandler input, IAuthorizationService auth, IConsole console, IContext context)
    {
        _input = input;
        _auth = auth;
        _console = console;
        _context = context;
    }
    public void Execute()
    {
        string email = _input.GetString("Enter email address", new EmailValidator());
        string password = _input.GetString("Enter password", new PasswordValidator());
        
        _auth.RegisterUser(email, password);
        
        _console.WriteLine("Registration successful");

        NextViewName = PageName.MainMenuPage;
    }
}