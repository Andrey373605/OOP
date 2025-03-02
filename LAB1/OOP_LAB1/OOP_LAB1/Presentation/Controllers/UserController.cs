using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Interfaces;
using OOP_LAB1.Presentation.Views;

namespace OOP_LAB1.Presentation.Controllers;

public class UserController
{
    private readonly IAuthorizationService _authorizationService;
    private readonly RegistrationView _registrationView;

    public UserController(IAuthorizationService authorizationService)
    {
        _authorizationService = authorizationService;
        _registrationView = new RegistrationView();
    }

    public void RegisterUser()
    {
        string firstName = _registrationView.GetFirstName();
        string lastName = _registrationView.GetLastName();
        string middleName = _registrationView.GetMiddleName();
        string phoneNumber = _registrationView.GetPhoneNumber();
        string series = _registrationView.GetPassportSeries();
        string identificationNumber = _registrationView.GetIdentificationNumber();
        string email = _registrationView.GetEmail();
        string password = _registrationView.GetPassword();
        

        try
        {
            _authorizationService.RegisterUserAsync(firstName, lastName, middleName, email, password,
                phoneNumber, identificationNumber, series);
            _registrationView.DisplayRegistrationSuccess();
        }
        catch (Exception ex)
        {
            _registrationView.DisplayRegistrationFailure();
        }
    }

    public void AuthenticateUser()
    {
        
    }
}
