namespace OOP_LAB1.Domain.Interfaces;

public interface IAuthorizationService
{
    Task RegisterUserAsync(string fisrtName, string lastName, string middleName, string email, string password,
        string phoneNumber, string passportNumber, string passportSeries);

    Task<bool> AuthenticateUserAsync(string email, string password);
    
    Task ApproveRequestUserRegistrationAsync(int id);
}