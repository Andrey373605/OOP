using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views;

public class RegistrationView : IView
{
    private readonly IInputHandler _input;
    private readonly IAuthorizationService _auth;
    private readonly IConsoleView _console;
    
    public PageName? NextViewName { get; private set; }

    public RegistrationView(IInputHandler input, IConsoleView console, IAuthorizationService authService)
    {
        _input = input;
        _auth = authService;
        _console = console;
    }

    public void Execute()
    {
        string firstName = _input.GetString("First name: ", new NameValidator());
        string lastName = _input.GetString("Last name: ", new NameValidator());
        string middleName = _input.GetString("Middle name: ", new NameValidator());
        string email = _input.GetString("Email: ", new EmailValidator());
        string phoneNumber = _input.GetString("Phone number: ", new PhoneValidator());
        string series = _input.GetString("Series: ", new SeriesValidator());
        string identificationNumber = _input.GetString("Identification number: ", new IdentificationNumberValidator());
        string password = _input.GetString("Password: ", new PasswordValidator());
        
        try
        {
            _auth.RegisterClientAsync(firstName, lastName, middleName, email, password, phoneNumber,
                identificationNumber, series).GetAwaiter().GetResult();
            NextViewName = PageName.BankChoosePage;
            _console.WriteLine("Registration successful!");
        }
        catch (Exception ex)
        {
            _console.WriteLine($"Error: {ex.Message}");
        }
        
    }
}