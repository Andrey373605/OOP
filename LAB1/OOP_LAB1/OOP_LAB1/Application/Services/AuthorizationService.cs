using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Interfaces;
using System.Security.Cryptography;
using System.Text;
using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;


namespace OOP_LAB1.Application.Services;

public class AuthorizationService : IAuthorizationService
{
    IUserRepository _userRepository;
    IClientRepository _clientRepository;
    IContext _context;

    public AuthorizationService(IUserRepository userRepository, IClientRepository clientRepository,IContext context)
    {
        _userRepository = userRepository;
        _clientRepository = clientRepository;
        _context = context;
    }

    public async Task RegisterClientAsync(string fisrtName, string lastName, string middleName, string email,
        string password, string phoneNumber, string passportNumber, string passportSeries)
    {
        var existingUser = await _userRepository.GetByEmailAsync(email);
        if (existingUser != null)
        {
            throw new Exception("User with this email already exists.");
        }

        var hashPassword = HashPassword(password);
        var registrationRequest = new Client
        {
            FirstName = fisrtName,
            LastName = lastName,
            MiddleName = middleName,
            Phone = phoneNumber,
            PassportSeries = passportSeries,
            IdentificationNumber = passportNumber
        };
        
        await _userRepository.CreateRequestAsync(registrationRequest);
    }

    public async Task RegisterEmployeeAsync(string email, string password, UserRole role)
    {
        var existingUser = await _userRepository.GetByEmailAsync(email);
        if (existingUser != null)
        {
            throw new Exception("Employee with this email already exists.");
        }
        
        var hashPassword = HashPassword(password);
        var employee = new User
        {
            Email = email,
            HashPassword = hashPassword,
            Role = role
        };
        
        await _userRepository.AddAsync(employee);
    }
    

    public async Task<bool> AuthorizeEmployeeAsync(string email, string password)
    {
        var employee = await _userRepository.GetByEmailAsync(email);
        if (employee == null)
        {
            return false;
        }
        
        var hashPassword = HashPassword(password);
        _context.SetCurrent(employee);
        return employee.HashPassword == hashPassword;
    }


    public async Task<bool> AuthenticateUserAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null)
        {
            return false;
        }

        var hashPassword = HashPassword(password);
        _context.SetCurrent(user);
        return user.HashPassword == hashPassword;
    }
    

    public async Task ApproveRequestClientRegistrationAsync(int id)
    {
        var client = await _clientRepository.GetRequestByIdAsync(id);
        if (client == null)
        {
            throw new Exception("Request does not exist.");
        }
        client.IsActive = true;

        await _clientRepository.UpdateAsync(client);
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
    
}