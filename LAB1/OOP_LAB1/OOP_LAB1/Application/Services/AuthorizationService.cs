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
    IEmployeeRepository _employeeRepository;
    UserContext _userContext;
    EmployeeContext _employeeContext;

    public AuthorizationService(IUserRepository userRepository, IEmployeeRepository employeeRepository)
    {
        _userRepository = userRepository;
        _employeeRepository = employeeRepository;
        _userContext = new UserContext();
        _employeeContext = new EmployeeContext();
    }

    public async Task RegisterUserAsync(string fisrtName, string lastName, string middleName, string email,
        string password, string phoneNumber, string passportNumber, string passportSeries)
    {
        var existingUser = await _userRepository.GetUserByEmailAsync(email);
        if (existingUser != null)
        {
            throw new Exception("User with this email already exists.");
        }

        var hashPassword = HashPassword(password);
        var registrationRequest = new RegistrationRequest
        {
            FirstName = fisrtName,
            LastName = lastName,
            MiddleName = middleName,
            Email = email,
            HashPassword = hashPassword,
            Phone = phoneNumber,
            PassportSeries = passportSeries,
            IdentificationNumber = passportNumber
        };

        await _userRepository.CreateRequestAsync(registrationRequest);
    }

    public async Task RegisterEmployeeAsync(string email, string password, EmployeeRole role)
    {
        var existingEmployee = await _employeeRepository.GetByEmailAsync(email);
        if (existingEmployee != null)
        {
            throw new Exception("Employee with this email already exists.");
        }
        
        var hashPassword = HashPassword(password);
        var employee = new Employee
        {
            Email = email,
            HashPassword = hashPassword,
            Role = role
        };
        
        await _employeeRepository.AddAsync(employee);
    }

    public async Task<bool> AuthorizeEmployeeAsync(string email, string password)
    {
        var employee = await _employeeRepository.GetByEmailAsync(email);
        if (employee == null)
        {
            return false;
        }
        
        var hashPassword = HashPassword(password);
        _employeeContext.SetCurrent(employee);
       _userContext.ClearCurrent();
        return employee.HashPassword == hashPassword;
    }


    public async Task<bool> AuthenticateUserAsync(string email, string password)
    {
        var user = await _userRepository.GetUserByEmailAsync(email);
        if (user == null)
        {
            return false;
        }

        var hashPassword = HashPassword(password);
        _userContext.SetCurrent(user);
        _employeeContext.ClearCurrent();
        return user.HashPassword == hashPassword;
    }
    

    public async Task ApproveRequestUserRegistrationAsync(int id)
    {
        RegistrationRequest registrationRequest = await _userRepository.GetRequestByIdAsync(id);
        if (registrationRequest == null)
        {
            throw new Exception("Request does not exist.");
        }

        User user = new User
        {
            Id = id,
            FirstName = registrationRequest.FirstName,
            LastName = registrationRequest.LastName,
            MiddleName = registrationRequest.MiddleName,
            Email = registrationRequest.Email,
            HashPassword = registrationRequest.HashPassword,
            Phone = registrationRequest.Phone,
            PassportSeries = registrationRequest.PassportSeries,
            IdentificationNumber = registrationRequest.IdentificationNumber
        };

        await _userRepository.CreateAsync(user);
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