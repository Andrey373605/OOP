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
    private readonly IConsoleView _console;
    private readonly IContext _context;
    
    public PageName? NextViewName { get; private set; }

    public RegistrationClientView(IInputHandler input, IConsoleView console, IAuthorizationService authService, IContext context)
    {
        _input = input;
        _auth = authService;
        _console = console;
        _context = context;
    }

    public void Execute()
    {
        string firstName = _input.GetString("First name: ", new NameValidator());
        string lastName = _input.GetString("Last name: ", new NameValidator());
        string middleName = _input.GetString("Middle name: ", new NameValidator());
        string phoneNumber = _input.GetString("Phone number: ", new PhoneValidator());
        string series = _input.GetString("Series: ", new SeriesValidator());
        string identificationNumber = _input.GetString("Identification number: ", new IdentificationNumberValidator());

        var user = _context.CurrentUser;
        
        try
        {
            _auth.RegisterClientAsync(user, firstName, lastName, middleName, phoneNumber,
                identificationNumber, series).GetAwaiter().GetResult();
            NextViewName = PageName.RegisterInBankPage;
            _console.WriteLine("Registration successful!");
        }
        catch (Exception ex)
        {
            _console.WriteLine($"Error: {ex.Message}");
        }
        
    }
}