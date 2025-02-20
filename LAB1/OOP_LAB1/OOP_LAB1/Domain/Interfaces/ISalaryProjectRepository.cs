using OOP_LAB1.Domain.Enteties;


namespace OOP_LAB1.Domain.Interfaces
{
    internal interface ISalaryProjectRepository
    {
        void Add(SalaryProject salaryProject);

        SalaryProject GetById(int id);

        void Update(SalaryProject salaryProject);

        void Delete(int id);

        IEnumerable<SalaryProject> GetAll();

        IEnumerable<SalaryProject> GetByEnterpriseId(int enterpriseId);

        IEnumerable<SalaryProject> GetByBankId(int bankId);
    }
}
