using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    public Task CreateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public async Task CreateRequestAsync(RegistrationRequest request)
    {
        return;
    }

    public Task<RegistrationRequest> GetRequestByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<User> GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<User>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<User> GetUserByEmailAsync(string email)
    {
        await Task.Delay(1);
        return null;
    }
}