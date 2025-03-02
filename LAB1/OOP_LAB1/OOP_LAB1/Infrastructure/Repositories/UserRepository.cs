using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    public Task CreateAsync(User user)
    {
        throw new NotImplementedException();
    }

    public Task CreateRequestAsync(RegistrationRequest request)
    {
        throw new NotImplementedException();
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

    public Task<User> GetUserByEmailAsync(string email)
    {
        throw new NotImplementedException();
    }
}