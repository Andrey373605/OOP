using System.Net.Sockets;
using OOP_LAB1.Domain.Entities;
namespace OOP_LAB1.Application.Interfaces;

public interface IAccountRepository
{
    public Task UpdateAsync(Account account);
    public Task AddAsync(Account account);
    public Task RemoveAsync(Account account);
    public Task<Account> GetByIdAsync(int id);
}