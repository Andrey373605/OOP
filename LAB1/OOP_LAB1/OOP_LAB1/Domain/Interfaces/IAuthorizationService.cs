using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
namespace OOP_LAB1.Domain.Interfaces;


public interface IAuthorizationService
{
    Task RegisterUser(string email, string password);
    Task RegisterClientAsync(User user, string fisrtName, string lastName, string middleName, 
        string phoneNumber, string passportNumber, string passportSeries);
    Task RegisterEmployeeAsync(User user, UserRole role);

    Task<bool> AuthenticateUserAsync(string email, string password);
    
    Task<User> GetUserByEmaiAsync(string email);
    
    Task ApproveRequestClientRegistrationAsync(int id);             
}