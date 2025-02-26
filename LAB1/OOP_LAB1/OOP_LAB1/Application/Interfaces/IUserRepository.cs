﻿using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;

namespace OOP_LAB1.Application.Interfaces
{
    internal interface IUserRepository
    {
        Task CreateAsync(User user);
        
        Task CreateRequestAsync(RegistrationRequest request);
        
        Task<RegistrationRequest> GetRequestByIdAsync(int id);

        Task<User> GetByIdAsync(int id);

        Task UpdateAsync(User user);

        Task DeleteAsync(int id);

        Task<IEnumerable<User>> GetAllAsync();
        

        Task<User> GetUserByEmailAsync(string email);
        
    }
}
