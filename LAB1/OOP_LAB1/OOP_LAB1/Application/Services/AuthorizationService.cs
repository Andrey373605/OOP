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
    private readonly IUserRepository _userRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IBankRepository _bankRepository;

    public AuthorizationService(IUserRepository userRepository, IClientRepository clientRepository, 
        IEmployeeRepository employeeRepository, IBankRepository bankRepository)
    {
        _userRepository = userRepository;
        _clientRepository = clientRepository;
        _employeeRepository = employeeRepository;
        _bankRepository = bankRepository;
    }
    
    
    public async Task RegisterUser(string email, string password)
    {
        var existUser = await _userRepository.GetByEmailAsync(email);
        if (existUser != null)
        {
            throw new ApplicationException("User with this email already exists");
        }
        
        var user = new User
        {
            Email = email,
            HashPassword = HashPassword(password),
        };
        
        await _userRepository.AddAsync(user);
    }


    public async Task RegisterClientAsync(int userId, int bankId, string firstName, string lastName, string middleName, string phoneNumber,
        string passportNumber, string passportSeries)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("Context user error");
        }

        var bank = await _bankRepository.GetByIdAsync(bankId);
        if (bank == null)
        {
            throw new Exception("Context bank error");
        }
        
        var existClient = _bankRepository.GetClientByUserIdAsync(bank.Id, user.Id);
        if (existClient != null)
        {
            throw new ApplicationException("You are already a bank client");
        }
        
        var existEmployee = _bankRepository.GetEmployeeByUserIdAsync(bank.Id, user.Id);
        if (existEmployee != null)
        {
            throw new ApplicationException("You are a bank employee");
        }

        var client = new Client
        {
            FirstName = firstName,
            LastName = lastName,
            MiddleName = middleName,
            Phone = phoneNumber,
            PassportSeries = passportSeries,
            IdentificationNumber = passportNumber,
            UserId = user.Id,
            BankId = bank.Id,
        };
        await _clientRepository.AddAsync(client);
    }
    
    public async Task<Client> AuthenticateClientAsync(int userId, int bankId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Context user error");
        }
        
        var bank = await _bankRepository.GetByIdAsync(bankId);
        if (bank == null)
        {
            throw new UnauthorizedAccessException("Context bank error");
        }
        
        var existEmployee = _bankRepository.GetEmployeeByUserIdAsync(bank.Id, user.Id);
        if (existEmployee != null)
        {
            throw new ApplicationException("You are a bank employee");
        }
        
        var client = await _bankRepository.GetClientByUserIdAsync(bank.Id, user.Id);
        if (client == null)
        {
            throw new UnauthorizedAccessException("Client is not registered with this bank");
        }

        return client;
    }

    public async Task RegisterEmployeeAsync(int userId, int bankId, UserRole role)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new Exception("Context user error");
        }
        
        var bank = await _bankRepository.GetByIdAsync(bankId);
        if (bank == null)
        {
            throw new Exception("Context bank error");
        }
        
        var existClient = _bankRepository.GetClientByUserIdAsync(bank.Id, user.Id);
        if (existClient != null)
        {
            throw new ApplicationException("You are a bank client");
        }
        
        var existEmployee = _bankRepository.GetEmployeeByUserIdAsync(bank.Id, user.Id);
        if (existEmployee != null)
        {
            throw new ApplicationException("You are already a bank employee");
        }

        var employee = new Employee
        {
            Role = role,
            UserId = user.Id,
        };
        await _employeeRepository.AddAsync(employee);
    }

    public async Task<Employee> AuthenticateEmployeeAsync(int userId, int bankId)
    {
        var user = await _userRepository.GetByIdAsync(userId);
        if (user == null)
        {
            throw new UnauthorizedAccessException("Context user error");
        }
        
        var bank = await _bankRepository.GetByIdAsync(bankId);
        if (bank == null)
        {
            throw new UnauthorizedAccessException("Context bank error");
        }

        var existClient = _bankRepository.GetClientByUserIdAsync(bank.Id, user.Id);
        if (existClient != null)
        {
            throw new ApplicationException("You are already a bank client");
        }
        
        var employee = await _bankRepository.GetEmployeeByUserIdAsync(bank.Id, user.Id);
        if (employee == null)
        {
            throw new UnauthorizedAccessException("Employee does not register in this bank");
        }


        return employee;
    }


    public async Task<User> AuthenticateUserAsync(string email, string password)
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

        return user;
    }

    

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(hashedBytes);
        }
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