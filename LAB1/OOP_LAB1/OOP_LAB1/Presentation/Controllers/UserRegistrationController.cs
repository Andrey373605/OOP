using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Console;
using OOP_LAB1.Presentation.ValidatorInterfaces;
using OOP_LAB1.Presentation.Validators;

namespace OOP_LAB1.Presentation.Controllers;

public class UserRegistrationController
{
    private readonly IAuthorizationService _authorizationService;
    private readonly IConsoleView _consoleView;

    public UserRegistrationController(IAuthorizationService authorizationService, IConsoleView consoleView)
    {
        _authorizationService = authorizationService;
        _consoleView = consoleView;
    }

    public async Task RegisterUser()
    {
        string firstName = CheckInput(new NameValidator());
        string lastName = CheckInput(new NameValidator());
        string middleName = CheckInput(new NameValidator());
        string phoneNumber = CheckInput(new PhoneValidator());
        string series = CheckInput(new SeriesValidator());
        string identificationNumber = CheckInput(new IdentificationNumberValidator());
        string email = CheckInput(new EmailValidator());
        string password = CheckInput(new PasswordValidator());
        
        try
        {
            await _authorizationService.RegisterUserAsync(firstName, lastName, middleName, email, password,
                phoneNumber, identificationNumber, series);
            _consoleView.WriteLine($"User {firstName} {lastName} successfully registered.");
        }
        catch (Exception ex)
        {
            _consoleView.WriteLine($"Error: {ex.Message}");
        }
        
    }
    
    public string CheckInput(IStringValidator validator)
    {
        string inputString;
        while (true)
        {
            inputString = _consoleView.ReadLine();
            if (validator.IsValid(inputString))
            {
                break;
            }
            _consoleView.WriteLine(validator.GetInvalidValidationString());
        }

        return inputString;
    }
    
}
