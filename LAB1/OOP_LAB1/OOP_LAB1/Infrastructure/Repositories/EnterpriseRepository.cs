using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Infrastructure.Data;

namespace OOP_LAB1.Infrastructure.Repositories;

public class EnterpriseRepository : IEnterpriseRepository
{
    private readonly IDataBaseHelper _dataBaseHelper;

    public EnterpriseRepository(IDataBaseHelper dataBaseHelper)
    {
        _dataBaseHelper = dataBaseHelper;
    }

    public async Task<Enterprise> GetByIdAsync(int enterpriseId)
    {
        
        var query = "SELECT * FROM Enterprise WHERE Id = @Id";
        var parameters = new Dictionary<string, object> { ["Id"] = enterpriseId };

        var result = await Task.Run(()=>_dataBaseHelper.ExecuteQuery(query, parameters).FirstOrDefault());

        if (result == null)
            return null;

        return new Enterprise
        {
            Id = Convert.ToInt32(result["Id"]),
            Type = Convert.ToString(result["Type"]),
            LegalName = Convert.ToString(result["LegalName"]),
            UNP = Convert.ToString(result["UNP"]),
            Adress = Convert.ToString(result["Adress"]),
            BankId = Convert.ToInt32(result["BankId"])
        };
    }

    public async Task<IEnumerable<Enterprise>> GetAllEnterprisesAsync()
    {
        
        var query = "SELECT * FROM Enterprise";
        var parameters = new Dictionary<string, object>();

        var results = await Task.Run(()=>_dataBaseHelper.ExecuteQuery(query, parameters));
        
        var enterprises = new List<Enterprise>();

        foreach (var result in results)
        {
            enterprises.Add(new Enterprise
            {
                Id = Convert.ToInt32(result["Id"]),
                Type = Convert.ToString(result["Type"]),
                LegalName = Convert.ToString(result["LegalName"]),
                UNP = Convert.ToString(result["UNP"]),
                Adress = Convert.ToString(result["Adress"]),
                BankId = Convert.ToInt32(result["BankId"])
            });
        }

        return enterprises;

    }
}