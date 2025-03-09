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
    IEmployeeRepository _employeeRepository;
    IBankRepository _bankRepository;
    IContext _context;

    public AuthorizationService(IUserRepository userRepository, IClientRepository clientRepository,
        IContext context, IEmployeeRepository employeeRepository, IBankRepository bankRepository)
    {
        _userRepository = userRepository;
        _clientRepository = clientRepository;
        _context = context;
        _employeeRepository = employeeRepository;
        _bankRepository = bankRepository;
    }
    
    
    public async Task RegisterUser(string email, string password)
    {
        var User = new User
        {
            Email = email,
            HashPassword = HashPassword(password),
        };
        
        await _userRepository.AddAsync(User);
    }

    public async Task RegisterClientAsync(string fisrtName, string lastName, string middleName, string phoneNumber,
        string passportNumber, string passportSeries)
    {
        var user = _context.CurrentUser;
        if (user == null)
        {
            throw new Exception("Context user error");
        }

        var client = new Client
        {
            FirstName = fisrtName,
            LastName = lastName,
            MiddleName = middleName,
            Phone = phoneNumber,
            PassportSeries = passportSeries,
            IdentificationNumber = passportNumber,
            UserId = user.Id,
        };
        await _clientRepository.AddAsync(client);
    }
    
    public async Task<bool> AuthenticateClientAsync()
    {
        var user = _context.CurrentUser;
        if (user == null)
        {
            throw new UnauthorizedAccessException("Context user error");
        }
        var bank = _context.CurrentBank;
        if (bank == null)
        {
            throw new UnauthorizedAccessException("Context bank error");
        }
        var client = await _bankRepository.GetClientByUserIdAsync(user.Id);
        if (client == null)
        {
            throw new UnauthorizedAccessException("Client is not registered with this bank");
        }
        
        return true;
    }

    public async Task RegisterEmployeeAsync(UserRole role)
    {
        var user = _context.CurrentUser;
        if (user == null)
        {
            throw new Exception("Context user error");
        }

        var employee = new Employee
        {
            Role = role,
            UserId = user.Id,
        };
        await _employeeRepository.AddAsync(employee);
    }

    public async Task<bool> AuthenticateEmployeeAsync()
    {
        var user = _context.CurrentUser;
        if (user == null)
        {
            throw new UnauthorizedAccessException("Context user error");
        }
        var bank = _context.CurrentBank;
        if (bank == null)
        {
            throw new UnauthorizedAccessException("Context bank error");
        }
        var employee = await _bankRepository.GetEmployeeByUserIdAsync(user.Id);
        if (employee == null)
        {
            throw new UnauthorizedAccessException("Employee does not register in this bank");
        }
        
        return true;
    }


    public async Task<bool> AuthenticateUserAsync(string email, string password)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null)
        {
            throw new UnauthorizedAccessException("User with such email does not exist");
        }

        var hashPassword = HashPassword(password);
        if (hashPassword != user.HashPassword)
        {
            throw new UnauthorizedAccessException("Invalid password");
        }
        
        _context.SetCurrent(user);
        return true;
    }

    

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
    }
    
    public void LoginBank(Bank bank)
    {
        _context.SetCurrent(bank);
    }
    
    public async Task ApproveRegistrationClient(int id)
    {
        var client = await _clientRepository.GetByIdAsync(id);
        if (client == null)
        {
            throw new Exception("User does not exist");
        }
        client.Activate();
        await _clientRepository.UpdateAsync(client);
    }
    
}