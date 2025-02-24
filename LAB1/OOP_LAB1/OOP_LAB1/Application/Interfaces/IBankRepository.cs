using OOP_LAB1.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LAB1.Application.Interfaces
{
    internal interface IBankRepository
    {
        void Add(Bank bank);

        Bank GetById(int id);

        void Update(Bank bank);


        
    }
}
