using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Infrastructure.Data;

namespace OOP_LAB1.Infrastructure.Repositories;

public class BankRepository : IBankRepository
{
    private readonly DatabaseHelper _databaseHelper;

    public BankRepository(string connectionString)
    {
        _databaseHelper = new DatabaseHelper(connectionString);
    }
    public void Add(Bank bank)
    {
        var query = @"
            INSERT INTO Banks (Name, IsEnterprise, EnterpriseType, LegalName, UNP, BIK, Address)
            VALUES (@Name, @IsEnterprise, @EnterpriseType, @LegalName, @UNP, @BIK, @Address)";

        var parameters = new Dictionary<string, object>
        {
            { "@Name", bank.Name },
            { "@IsEnterprise", bank.IsEnterprise },
            { "@EnterpriseType", bank.EnterpriseType },
            { "@LegalName", bank.LegalName },
            { "UNP", bank.UNP },
            { "@BIK", bank.BIK },
            { "@Address", bank.Address }
        };
        
        _databaseHelper.ExecuteNonQuery(query, parameters);
        bank.Id = _databaseHelper.GetLastInsertId();
    }

    public Bank GetById(int id)
    {
        var query = @"
                SELECT * 
                FROM Bank WHERE     
                Id = @Id";

        var parameters = new Dictionary<string, object>
        {
            { "Id", id }
        };
        
        var dataTable = _databaseHelper.ExecuteQuery(query, parameters);

        if (dataTable.Count == 0)
        {
            return null!;
        }
        
        var row = dataTable[0];
        return new Bank
        {
            Id = Convert.ToInt32(row["Id"]),
            Name = row["Name"].ToString(),
            Address = row["Address"].ToString(),
            IsEnterprise = Convert.ToBoolean(row["IsEnterprise"]),
            EnterpriseType = row["Enterprise"]?.ToString(),
            LegalName = row["LegalName"]?.ToString(),
            UNP = row["UNP"]?.ToString(),
            BIK = row["BIK"]?.ToString(),
            UsersIdList = GetUsersIdListForBank(id),
            EnterprisesIdList = GetEnterprisesIdListForBank(id)
        };
    }

    private List<int> GetEnterprisesIdListForBank(int id)
    {
        var query = @"
            SELECT EnterpriseId
            FROM BankEnterprises
            WHERE BankId = @Id";

        var paramaters = new Dictionary<string, object>
        {
            { "@Id", id }
        };
        var dataTable = _databaseHelper.ExecuteQuery(query, paramaters);
        return dataTable.Select(u => Convert.ToInt32(u["EnterpriseId"])).ToList();
    }

    private List<int> GetUsersIdListForBank(int id)
    {
        var query = @"
            SELECT DISTINCT U.Id
            FROM Users U
            INNER JOIN Accounts A ON U.Id = A.OwnerId
            WHERE A.BankId = @Id";

        var paramaters = new Dictionary<string, object>
        {
            { "@Id", id }
        };
        var dataTable = _databaseHelper.ExecuteQuery(query, paramaters);
        return dataTable.Select(u => Convert.ToInt32(u["Id"])).ToList();
    }


    public void Update(Bank bank)
    {
        throw new NotImplementedException();
    }

    public void Delete(int id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Bank> GetAll()
    {
        throw new NotImplementedException();
    }

    public Bank GetByName(string name)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Enterprise> GetEnterprisesByBankId(int bankId)
    {
        throw new NotImplementedException();
    }
}