using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LAB1.Domain.Enteties
{
    internal class SalaryProject
    {
        public int Id { get; set; }
        public Enterprise Enterprise { get; set; }
        public Bank Bank { get; set; }
        public List<Account> EmployeeAccounts { get; set; } = new();
        public bool IsActive { get; set; }
    }
}
