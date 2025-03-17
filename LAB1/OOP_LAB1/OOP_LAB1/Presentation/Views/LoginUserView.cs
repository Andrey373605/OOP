using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Navigator;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views;

[ViewMapping(PageName.LoginUserPage)]
public class LoginUserView : IView
{
    private readonly IInputHandler _input;
    private readonly IAuthorizationService _auth;
    private readonly IConsole _console;
    private readonly IApplicationService _applicationService;

    public LoginUserView(IInputHandler input, IAuthorizationService auth, IConsole console, IApplicationService applicationService)
    {
        _input = input;
        _auth = auth;
        _console = console;
        _applicationService = applicationService;
    }
    public PageName? NextViewName { get; private set; }
    public async Task Execute()
    {
        string email = _input.GetString("Email: ", new EmailValidator());
        string password = _input.GetString("Password: ", new PasswordValidator());

        _console.Clear();
        try
        {
            await _applicationService.LoginUser(email, password);
            _console.WriteLine($"Successfully logged in");
            NextViewName = PageName.ChooseBankPage;
        }
        catch (Exception e)
        {
            _console.WriteLine(e.Message);
            NextViewName = PageName.MainMenuPage;
        }
        
        
    }
}