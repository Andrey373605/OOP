
namespace OOP_LAB1.Domain.Entities
{
    public class Bank
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<int> UsersIdList { get; set; } = new();
        public List<int> EnterprisesIdList { get; set; } = new();
        
        
        public bool IsEnterprise { get; set; } = false; // Флаг, указывающий, является ли банк предприятием
        public string? EnterpriseType { get; set; } // Тип предприятия (например, ООО, ИП)
        public string? LegalName { get; set; } // Юридическое название предприятия
        public string? UNP { get; set; } // УНП предприятия
        public string? BIK { get; set; } // БИК предприятия
        public string? Address { get; set; } // Адрес предприятия
    }
}
