using OOP_LAB1.Domain.Enteties;
using OOP_LAB1.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LAB1.Application.Interfaces
{
    internal interface ILoanReository
    {
        void Add(Loan loan);

        Loan GetById(int id);

        void Update(Loan loan);

        void Delete(int id);

        IEnumerable<Loan> GetAll();

        IEnumerable<Loan> GetByOwnerId(int ownerId);

        IEnumerable<Loan> GetByStatus(LoanStatus status);
    }
}
