using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
namespace OOP_LAB1.Domain.Interfaces;


public interface IAuthorizationService
{
    Task RegisterUser(string email, string password);
    Task<bool> AuthenticateUserAsync(IContext context, string email, string password);
    
    
    
    Task RegisterClientAsync(IContext context, string firstName, string lastName, string middleName, 
        string phoneNumber, string passportNumber, string passportSeries);
    Task ApproveRegistrationClient(int id);
    Task<bool> AuthenticateClientAsync(IContext context);

    
    Task RegisterEmployeeAsync(IContext context, UserRole role);
    Task<bool> AuthenticateEmployeeAsync(IContext context);
    
    void LoginBank(IContext context, Bank bank);
}