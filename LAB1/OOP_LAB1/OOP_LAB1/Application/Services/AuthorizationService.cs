using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Interfaces;
using System.Security.Cryptography;
using System.Text;
using OOP_LAB1.Application.Context;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
using Serilog;
using System;

namespace OOP_LAB1.Application.Services;

public class AuthorizationService : IAuthorizationService
{
    private readonly IUserRepository _userRepository;
    private readonly IClientRepository _clientRepository;
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IBankRepository _bankRepository;
    private readonly ILogger _logger;

    public AuthorizationService(IUserRepository userRepository, IClientRepository clientRepository, 
        IEmployeeRepository employeeRepository, IBankRepository bankRepository, ILogger logger)
    {
        _userRepository = userRepository;
        _clientRepository = clientRepository;
        _employeeRepository = employeeRepository;
        _bankRepository = bankRepository;
        _logger = logger;
    }
    
    public async Task RegisterUser(string email, string password)
    {
        try
        {
            _logger.Information("Attempting to register user with email: {Email}", email);
            
            var existUser = await _userRepository.GetByEmailAsync(email);
            if (existUser != null)
            {
                _logger.Warning("User with email {email} already exists", email);
                throw new ApplicationException("User with this email already exists");
            }
            
            var user = new User
            {
                Email = email,
                HashPassword = HashPassword(password),
            };
            
            await _userRepository.AddAsync(user);
            _logger.Information("User with email {Email} successfully registered", email);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error registering user with email {Email}", email);
            throw;
        }
    }

    public async Task RegisterClientAsync(int userId, int bankId, string firstName, string lastName, string middleName, string phoneNumber,
        string passportNumber, string passportSeries)
    {
        try
        {
            _logger.Information("Attempting to register client with user ID: {UserId} and bank ID: {BankId}", userId, bankId);
            
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                _logger.Warning("User with ID {UserId} does not exist", userId);
                throw new Exception("Context user error");
            }

            var bank = await _bankRepository.GetByIdAsync(bankId);
            if (bank == null)
            {
                _logger.Warning("Bank with ID {BankId} does not exist", bankId);
                throw new Exception("Context bank error");
            }
            
            var existClient = await _clientRepository.GetClientByUserIdAsync(bank.Id, user.Id);
            if (existClient != null)
            {
                _logger.Warning("User with ID {UserId} is already a bank client", userId);
                throw new ApplicationException("You are already a bank client");
            }
            
            var existEmployee = await _employeeRepository.GetEmployeeByUserIdAsync(bank.Id, user.Id);
            if (existEmployee != null)
            {
                _logger.Warning("User with ID {UserId} is a bank employee", userId);
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
                Status = ClientStatus.Application
            };
            await _clientRepository.AddAsync(client);
            _logger.Information("Client with user ID {UserId} successfully registered in bank with ID {BankId}", userId, bankId);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error registering client with user ID {UserId} and bank ID {BankId}", userId, bankId);
            throw;
        }
    }
    
    public async Task<Client> AuthenticateClientAsync(int userId, int bankId)
    {
        try
        {
            _logger.Information("Attempting to authenticate client with user ID: {UserId} and bank ID: {BankId}", userId, bankId);
            
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                _logger.Warning("User with ID {UserId} does not exist", userId);
                throw new UnauthorizedAccessException("Context user error");
            }
            
            var bank = await _bankRepository.GetByIdAsync(bankId);
            if (bank == null)
            {
                _logger.Warning("Bank with ID {BankId} does not exist", bankId);
                throw new UnauthorizedAccessException("Context bank error");
            }
            
            var existEmployee = await _employeeRepository.GetEmployeeByUserIdAsync(bank.Id, user.Id);
            if (existEmployee != null)
            {
                _logger.Warning("User with ID {UserId} is a bank employee", userId);
                throw new ApplicationException("You are a bank employee");
            }
            
            var client = await _clientRepository.GetClientByUserIdAsync(bank.Id, user.Id);
            if (client == null)
            {
                _logger.Warning("Client with user ID {UserId} is not registered with bank ID {BankId}", userId, bankId);
                throw new UnauthorizedAccessException("Client is not registered with this bank");
            }

            if (!client.IsActive())
            {
                _logger.Warning("Client with user ID {UserId} is not active", userId);
                throw new ApplicationException("Client is not active");
            }

            _logger.Information("Client with user ID {UserId} successfully authenticated in bank with ID {BankId}", userId, bankId);
            return client;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error authenticating client with user ID {UserId} and bank ID {BankId}", userId, bankId);
            throw;
        }
    }

    public async Task RegisterEmployeeAsync(int userId, int bankId, EmployeeRole role)
    {
        try
        {
            _logger.Information("Attempting to register employee with user ID: {UserId} and bank ID: {BankId}", userId, bankId);
            
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                _logger.Warning("User with ID {UserId} does not exist", userId);
                throw new Exception("Context user error");
            }
            
            var bank = await _bankRepository.GetByIdAsync(bankId);
            if (bank == null)
            {
                _logger.Warning("Bank with ID {BankId} does not exist", bankId);
                throw new Exception("Context bank error");
            }
            
            var existClient = await _clientRepository.GetClientByUserIdAsync(bank.Id, user.Id);
            if (existClient != null)
            {
                _logger.Warning("User with ID {UserId} is a bank client", userId);
                throw new ApplicationException("You are a bank client");
            }
            
            var existEmployee = await _employeeRepository.GetEmployeeByUserIdAsync(bank.Id, user.Id);
            if (existEmployee != null)
            {
                _logger.Warning("User with ID {UserId} is already a bank employee", userId);
                throw new ApplicationException("You are already a bank employee");
            }

            var employee = new Employee
            {
                Role = role,
                UserId = user.Id,
                Status = EmployeeStatus.Application
            };
            await _employeeRepository.AddAsync(employee);
            _logger.Information("Employee with user ID {UserId} successfully registered in bank with ID {BankId}", userId, bankId);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error registering employee with user ID {UserId} and bank ID {BankId}", userId, bankId);
            throw;
        }
    }

    public async Task<Employee> AuthenticateEmployeeAsync(int userId, int bankId)
    {
        try
        {
            _logger.Information("Attempting to authenticate employee with user ID: {UserId} and bank ID: {BankId}", userId, bankId);
            
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
            {
                _logger.Warning("User with ID {UserId} does not exist", userId);
                throw new UnauthorizedAccessException("Context user error");
            }
            
            var bank = await _bankRepository.GetByIdAsync(bankId);
            if (bank == null)
            {
                _logger.Warning("Bank with ID {BankId} does not exist", bankId);
                throw new UnauthorizedAccessException("Context bank error");
            }

            var existClient = await _clientRepository.GetClientByUserIdAsync(bank.Id, user.Id);
            if (existClient != null)
            {
                _logger.Warning("User with ID {UserId} is a bank client", userId);
                throw new ApplicationException("You are already a bank client");
            }
            
            var employee = await _employeeRepository.GetEmployeeByUserIdAsync(bank.Id, user.Id);
            if (employee == null || employee.Status != EmployeeStatus.Active)
            {
                _logger.Warning("Employee with user ID {UserId} is not active or does not exist", userId);
                throw new ApplicationException("Employee is not active");
            }
            
            _logger.Information("Employee with user ID {UserId} successfully authenticated in bank with ID {BankId}", userId, bankId);
            return employee;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error authenticating employee with user ID {UserId} and bank ID {BankId}", userId, bankId);
            throw;
        }
    }

    public async Task<User> AuthenticateUserAsync(string email, string password)
    {
        try
        {
            _logger.Information("Attempting to authenticate user with email: {Email}", email);
            
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null)
            {
                _logger.Warning("User with email {Email} does not exist", email);
                throw new UnauthorizedAccessException("User with such email does not exist");
            }

            var hashPassword = HashPassword(password);
            if (hashPassword != user.HashPassword)
            {
                _logger.Warning("Invalid password for user with email {Email}", email);
                throw new UnauthorizedAccessException("Invalid password");
            }

            _logger.Information("User with email {Email} successfully authenticated", email);
            return user;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error authenticating user with email {Email}", email);
            throw;
        }
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
        try
        {
            _logger.Information("Attempting to approve registration for client with ID: {ClientId}", id);
            
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null)
            {
                _logger.Warning("Client with ID {ClientId} does not exist", id);
                throw new Exception("User does not exist");
            }
            client.Activate();
            await _clientRepository.UpdateAsync(client);
            _logger.Information("Registration for client with ID {ClientId} successfully approved", id);
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error approving registration for client with ID {ClientId}", id);
            throw;
        }
    }
}