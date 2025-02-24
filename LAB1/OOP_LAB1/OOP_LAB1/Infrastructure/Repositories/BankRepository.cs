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
        var query = @"
            UPDATE Banks
            SET Name = @Name,
                IsEnterprise = @IsEnterprise,
                EnterpriseType = @EnterpriseType,
                LegalName = @LegalName,
                UNP = @UNP,
                BIK = @BIK,
                Address = @Address
            WHERE Id = @Id";

        var parameters = new Dictionary<string, object>
        {
            { "Id", bank.Id },
            { "Name", bank.Name },
            { "IsEnterprise", bank.IsEnterprise },
            { "EnterpriseType", bank.EnterpriseType },
            { "LegalName", bank.LegalName },
            { "UNP", bank.UNP },
            { "BIK", bank.BIK },
            { "Address", bank.Address }
        };
        
        _databaseHelper.ExecuteNonQuery(query, parameters);
        
        SynchronizeBankUsers(bank.Id, bank.UsersIdList);
        SynchronizeBankEnterprises(bank.Id, bank.EnterprisesIdList);
    }
    
    private void SynchronizeBankUsers(int bankId, IEnumerable<int> userIds)
    {
        var currentUserIdList = GetUsersIdListForBank(bankId);
        
        var usersToAdd = userIds.Except(currentUserIdList).ToList();
        var usersToRemove = currentUserIdList.Except(userIds).ToList();

        foreach (var user in usersToAdd)
        {
            AddUserToBank(bankId, user);
        }

        foreach (var user in usersToRemove)
        {
            RemoveUserFromBank(bankId, user);
        }
    }

    private void RemoveUserFromBank(int bankId, int userId)
    {
        var query = @"
            DELETE FROM Accounts
            WHERE OwnerId = @Id and IsUserOwner = 1";
        
        var parameters = new Dictionary<string, object>
        {
            { "OwnerId", userId }
        };
        _databaseHelper.ExecuteNonQuery(query, parameters);
    }

    private void AddUserToBank(int bankId, int userId)
    {
        var query = @"
            INSERT INTO Accounts (OwnerId, Balance, Type, IsBlocked, IsFrozen, IsUserOwner, IsEnterpriseOwner)
            VALUES (@OwnerId, 0, 'Deposit', 0, 0, 1, 0)";

        var parameters = new Dictionary<string, object>
        {
            { "OwnerId", userId }
        };

        _databaseHelper.ExecuteNonQuery(query, parameters);
    }
    
    private void SynchronizeBankEnterprises(int bankId, IEnumerable<int> enterpriseIds)
    {
        var currentEnterpriseIdList = GetEnterprisesIdListForBank(bankId);
        
        var enterprisesToAdd = enterpriseIds.Except(currentEnterpriseIdList).ToList();
        var enterprisesToRemove = currentEnterpriseIdList.Except(enterpriseIds).ToList();
        
        foreach (var enterpriseId in enterprisesToAdd)
        {
            AddEnterpriseToBank(bankId, enterpriseId);
        }
        
        foreach (var enterpriseId in enterprisesToRemove)
        {
            RemoveEnterpriseFromBank(bankId, enterpriseId);
        }
    }
    
    private void AddEnterpriseToBank(int bankId, int enterpriseId)
    {
        var query = @"
            INSERT INTO BankEnterprises (BankId, EnterpriseId)
            VALUES (@BankId, @EnterpriseId)";

        var parameters = new Dictionary<string, object>
        {
            { "BankId", bankId },
            { "EnterpriseId", enterpriseId }
        };

        _databaseHelper.ExecuteNonQuery(query, parameters);
    }

    private void RemoveEnterpriseFromBank(int bankId, int enterpriseId)
    {
        var query = @"
            DELETE FROM BankEnterprises
            WHERE BankId = @BankId AND EnterpriseId = @EnterpriseId";

        var parameters = new Dictionary<string, object>
        {
            { "BankId", bankId },
            { "EnterpriseId", enterpriseId }
        };

        _databaseHelper.ExecuteNonQuery(query, parameters);
    }
    
}