using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Application.Interfaces
{
    public interface IUserRepository
    {
        Task AddAsync(User user);
        
        Task CreateRequestAsync(Client client);
        
        Task<Client> GetRequestByIdAsync(int id);

        Task<User> GetByIdAsync(int id);

        Task UpdateAsync(User user);

        Task DeleteAsync(int id);

        Task<IEnumerable<User>> GetAllAsync();
        
        Task<User> GetByEmailAsync(string email);
    }
}
