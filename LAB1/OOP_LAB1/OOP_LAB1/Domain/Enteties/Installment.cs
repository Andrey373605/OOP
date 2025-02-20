using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_LAB1.Domain.Enteties
{
    internal class Installment
    {
        public int Id { get; set; }
        public Account Account { get; set; }
        public decimal TotalAmount { get; set; }
        public int NumberOfPayments { get; set; }
        public decimal MonthlyPayment => TotalAmount / NumberOfPayments;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public bool IsActive { get; set; }
    }
}
