using System.Net.Sockets;
using OOP_LAB1.Domain.Entities;
namespace OOP_LAB1.Application.Interfaces;

public interface IAccountRepository
{
    public void Update(Account account);
    public void Add(int userId);
    public void Remove(Account account);
    public Account GetById(int id);
}