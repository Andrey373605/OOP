using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
namespace OOP_LAB1.Domain.Interfaces;


public interface IAuthorizationService
{
    Task RegisterUser(string email, string password);
    Task<User> AuthenticateUserAsync(string email, string password);
    
    
    
    Task RegisterClientAsync(int userId, int bankId, string firstName, string lastName, string middleName, 
        string phoneNumber, string passportNumber, string passportSeries);
    Task ApproveRegistrationClient(int id);
    Task<Client> AuthenticateClientAsync(int userId, int bankId);

    
    Task RegisterEmployeeAsync(int userId, int bankId, EmployeeRole role);
    Task<Employee> AuthenticateEmployeeAsync(int userId, int bankId);
    
}