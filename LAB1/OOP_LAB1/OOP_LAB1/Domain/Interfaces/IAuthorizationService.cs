using OOP_LAB1.Domain.Enums;
namespace OOP_LAB1.Domain.Interfaces;


public interface IAuthorizationService
{
    Task RegisterClientAsync(string fisrtName, string lastName, string middleName, string email, string password,
        string phoneNumber, string passportNumber, string passportSeries);
    
    Task RegisterEmployeeAsync(string email, string password, UserRole role);
    
    Task<bool> AuthorizeEmployeeAsync(string email, string password);

    Task<bool> AuthenticateUserAsync(string email, string password);
    
    Task ApproveRequestClientRegistrationAsync(int id);
}