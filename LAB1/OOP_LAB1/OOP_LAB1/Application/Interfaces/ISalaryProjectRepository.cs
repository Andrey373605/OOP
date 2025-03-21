using System.Net.Sockets;
using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Application.Interfaces;

public interface ISalaryProjectRepository
{
    public Task AddAsync(SalaryProject salaryProject);
    
    public Task<SalaryProject> GetByIdAsync(int id);
    
    public Task UpdateAsync(SalaryProject salaryProject);
    

    public Task<IEnumerable<SalaryProject>> GetSalaryProjectRequests();
    public Task<IEnumerable<Salary>> GetSalaryRequests(int projectId);
    public Task<IEnumerable<Salary>> GetSalaries(int projectId);
    
    Task AddSalaryAsync(Salary salary);
    public Task UpdateSalaryAsync(Salary salary);
    public Task<Salary> GetSalaryAsync(int salaryId);
    public Task<IEnumerable<SalaryProject>> GetAllSalaryProjectAsync();
    public Task<IEnumerable<Salary>> GetAllSalaryRequests();
    Task<Salary> GetSalaryRequest(int salaryId);
}