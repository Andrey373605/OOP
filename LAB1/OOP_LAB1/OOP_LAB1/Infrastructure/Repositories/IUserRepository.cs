using OOP_LAB1.Domain.Enteties;
using OOP_LAB1.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LAB1.Infrastructure.Repositories
{
    internal interface IUserRepository
    {
        void Add(User user);

        User GetById(int id);

        void Update(User user);

        void Delete(int id);

        IEnumerable<User> GetAll();

        User GetByIdentificationNumber(string identificationNumber);

        User GetByEmail(string email);

        IEnumerable<User> GetUsersByRole(UserRole userRole);
    }
}
