using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LAB1.Domain.Enteties
{
    internal class Enterprise
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string LegalName { get; set; }
        public string UNP { get; set; }
        public string BIK { get; set; }
        public string Address { get; set; }
        public int BankId { get; set; }
        public Bank Bank { get; set; }
        public List<Account> Accounts { get; set; } = new();
    }
}
