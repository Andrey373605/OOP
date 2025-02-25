using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LAB1.Domain.Entities
{
    internal class SalaryProject
    {
        public int Id { get; set; }
        public int EnterpriseId { get; set; }
        public int BankId { get; set; }
        public bool IsActive { get; set; }
    }
}
