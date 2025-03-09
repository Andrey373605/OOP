using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.Enums;
using OOP_LAB1.Presentation.Handler;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Views;

public class RegistrationClientView : IView
{
    private readonly IInputHandler _input;
    private readonly IAuthorizationService _auth;
    private readonly IConsole _console;
    
    public PageName? NextViewName { get; private set; }

    public RegistrationClientView(IInputHandler input, IConsole console, IAuthorizationService authService)
    {
        _input = input;
        _auth = authService;
        _console = console;
    }

    public void Execute()
    {
        var firstName = _input.GetString("First name: ", new NameValidator());
        var lastName = _input.GetString("Last name: ", new NameValidator());
        var middleName = _input.GetString("Middle name: ", new NameValidator());
        var phoneNumber = _input.GetString("Phone number: ", new PhoneValidator());
        var series = _input.GetString("Series: ", new SeriesValidator());
        var identificationNumber = _input.GetString("Identification number: ", new IdentificationNumberValidator());
        
        try
        {
            _auth.RegisterClientAsync(firstName, lastName, middleName, phoneNumber,
                identificationNumber, series).GetAwaiter().GetResult();
            NextViewName = PageName.ChooseRolePage;
            _console.WriteLine("Registration successful!");
        }
        catch (Exception ex)
        {
            _console.WriteLine($"Error: {ex.Message}");
        }
        
    }
}