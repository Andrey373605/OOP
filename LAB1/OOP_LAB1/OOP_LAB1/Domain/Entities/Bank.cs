
namespace OOP_LAB1.Domain.Entities
{
    public class Bank
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<int> UsersIdList { get; set; } = new();
        public List<int> EnterprisesIdList { get; set; } = new();
        
        
        public bool IsEnterprise { get; set; } = false;
        public string? EnterpriseType { get; set; } 
        public string? LegalName { get; set; }
        public string? UNP { get; set; }
        public string? BIK { get; set; }
        public string? Address { get; set; }
    }
}
