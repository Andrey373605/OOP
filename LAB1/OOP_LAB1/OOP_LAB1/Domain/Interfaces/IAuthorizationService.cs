using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
namespace OOP_LAB1.Domain.Interfaces;


public interface IAuthorizationService
{
    Task RegisterUser(string email, string password);
    Task<bool> AuthenticateUserAsync(string email, string password);
    
    
    
    Task RegisterClientAsync(string firstName, string lastName, string middleName, 
        string phoneNumber, string passportNumber, string passportSeries);
    Task ApproveRegistrationClient(int id);
    Task<bool> AuthenticateClientAsync();

    
    Task RegisterEmployeeAsync(UserRole role);
    Task<bool> AuthenticateEmployeeAsync();
    
    void LoginBank(Bank bank);
}