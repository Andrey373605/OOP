using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Domain.Interfaces;
using Serilog;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OOP_LAB1.Application.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly ILogger _logger;

    public EmployeeService(IEmployeeRepository employeeRepository, ILogger logger)
    {
        _employeeRepository = employeeRepository;
        _logger = logger;
    }
    
    public async Task<EmployeeRole> GetEmployeeRole(int userId, int bankId)
    {
        try
        {
            _logger.Information($"Attempting to retrieve employee role for user ID: {userId} in bank ID: {bankId}");
            
            var employee = await _employeeRepository.GetEmployeeByUserIdAsync(bankId, userId);
            if (employee == null)
            {
                _logger.Error($"Employee with user ID {userId} not found in bank ID {bankId}");
                throw new NullReferenceException("Employee not found");
            }
            _logger.Information($"Successfully retrieved employee role for user ID {userId} in bank ID {bankId}");
            return employee.Role;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error retrieving employee role for user ID {userId} in bank ID {bankId}");
            throw;
        }
    }

    public async Task<Employee> GetEmployeeByUserIdAsync(int bankId, int userId)
    {
        try
        {
            _logger.Information($"Attempting to retrieve employee with user ID: {userId} in bank ID: {bankId}");
            
            var employee = await _employeeRepository.GetEmployeeByUserIdAsync(bankId, userId);
            if (employee == null)
            {
                _logger.Error($"Employee with user ID {userId} not found in bank ID {bankId}");
                throw new NullReferenceException("Employee not found");
            }
            _logger.Information($"Successfully retrieved employee with user ID {userId} in bank ID {bankId}");
            return employee;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error retrieving employee with user ID {userId} in bank ID {bankId}");
            throw;
        }
    }

    public async Task<IEnumerable<Employee>> GetClientRegistrationRequests()
    {
        try
        {
            _logger.Information("Attempting to retrieve employee registration requests");
            
            var requests = await _employeeRepository.GetEmployeeRequestsAsync();
            _logger.Information("Successfully retrieved employee registration requests");
            return requests;
        }
        catch (Exception ex)
        {
            _logger.Error(ex, "Error retrieving employee registration requests");
            throw;
        }
    }

    public async Task ApproveClientRegistration(int id)
    {
        try
        {
            _logger.Information($"Attempting to approve employee registration with ID: {id}");
            
            var request = await _employeeRepository.GetEmployeeRequestAsync(id);
            if (request == null)
            {
                _logger.Error($"Employee request with ID {id} not found");
                throw new NullReferenceException("Employee not found");
            }

            request.Activate();
            await _employeeRepository.UpdateAsync(request);
            _logger.Information($"Successfully approved employee registration with ID {id}");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error approving employee registration with ID {id}");
            throw;
        }
    }

    public async Task RejectClientRegistration(int id)
    {
        try
        {
            _logger.Information($"Attempting to reject employee registration with ID: {id}");
            
            var request = await _employeeRepository.GetEmployeeRequestAsync(id);
            if (request == null)
            {
                _logger.Error($"Employee request with ID {id} not found");
                throw new NullReferenceException("Employee not found");
            }

            request.Reject();
            await _employeeRepository.UpdateAsync(request);
            _logger.Information($"Successfully rejected employee registration with ID {id}");
        }
        catch (Exception ex)
        {
            _logger.Error(ex, $"Error rejecting employee registration with ID {id}");
            throw;
        }
    }
}