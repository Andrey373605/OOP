using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;

namespace OOP_LAB1.Application.Interfaces
{
    internal interface IUserRepository
    {
        Task CreateAsync(User user);
        
        Task CreateRequestAsync(RegistrationRequest request);
        
        Task<RegistrationRequest> GetRequestByIdAsync(int id);

        Task<User> GetByIdAsync(int id);

        Task Update(User user);

        Task Delete(int id);

        Task<IEnumerable<User>> GetAll();

        User GetByIdentificationNumber(string identificationNumber);

        Task<User> GetUserByEmailAsync(string email);

        IEnumerable<User> GetUsersByRole(UserRole userRole);
    }
}
