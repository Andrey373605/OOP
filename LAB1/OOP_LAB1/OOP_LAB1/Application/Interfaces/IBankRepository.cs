using OOP_LAB1.Domain.Entities;


namespace OOP_LAB1.Application.Interfaces
{
    public interface IBankRepository
    {
        public Task AddAsync(Bank bank);

        public Task<Bank> GetByIdAsync(int id);
        
        public Task<Bank> GetByNameAsync(string id);

        public Task UpdateAsync(Bank bank);

        public Task<IEnumerable<string>> GetAllBankNamesAsync();

        public Task<Employee> GetEmployeeByUserIdAsync(int userId, int bankId);
        public Task<Client> GetClientByUserIdAsync(int userId, int bankId);
    }
}
