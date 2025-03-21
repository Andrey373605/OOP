using OOP_LAB1.Application.Interfaces;
using OOP_LAB1.Domain.Entities;
using OOP_LAB1.Domain.Enums;
using OOP_LAB1.Infrastructure.Data;

namespace OOP_LAB1.Infrastructure.Repositories;

public class SalaryProjectRepository : ISalaryProjectRepository
{
    private readonly IDataBaseHelper _dataBaseHelper;

    public SalaryProjectRepository(IDataBaseHelper dataBaseHelper)
    {
        _dataBaseHelper = dataBaseHelper;
    }

    public async Task AddAsync(SalaryProject salaryProject)
    {
        var query = @"
            INSERT INTO SalaryProject (EnterpriseId, Balance, BankId, Status)
            VALUES (@EnterpriseId, @Balance, @BankId, @Status)";

        var parameters = new Dictionary<string, object>
        {
            ["EnterpriseId"] = salaryProject.EnterpriseId,
            ["Balance"] = salaryProject.Balance,
            ["BankId"] = salaryProject.BankId,
            ["Status"] = (int)salaryProject.Status
        };

        await Task.Run(()=>_dataBaseHelper.ExecuteNonQuery(query, parameters));
    }

    public async Task<SalaryProject> GetByIdAsync(int id)
    {
        var query = "SELECT * FROM SalaryProject WHERE Id = @Id AND Status = @Status";
        var parameters = new Dictionary<string, object>
        {
            ["Id"] = id,
            ["Status"] = (int)SalaryProjectStatus.Active
        };

        var result = await Task.Run(()=>_dataBaseHelper.ExecuteQuery(query, parameters).FirstOrDefault());

        if (result == null)
            return null;

        return new SalaryProject
        {
            Id = Convert.ToInt32(result["Id"]),
            EnterpriseId = Convert.ToInt32(result["EnterpriseId"]),
            Balance = Convert.ToDecimal(result["Balance"]),
            BankId = Convert.ToInt32(result["BankId"]),
            Status = (SalaryProjectStatus)Convert.ToInt32(result["Status"])
        };
    }

    public async Task UpdateAsync(SalaryProject salaryProject)
    {
        var query = @"
            UPDATE SalaryProject
            SET EnterpriseId = @EnterpriseId, Balance = @Balance, BankId = @BankId, Status = @Status
            WHERE Id = @Id";

        var parameters = new Dictionary<string, object>
        {
            ["Id"] = salaryProject.Id,
            ["EnterpriseId"] = salaryProject.EnterpriseId,
            ["Balance"] = salaryProject.Balance,
            ["BankId"] = salaryProject.BankId,
            ["Status"] = (int)salaryProject.Status
        };

        await Task.Run(()=>_dataBaseHelper.ExecuteNonQuery(query, parameters));
    }

    public async Task AddAccountToSalaryProjectAsync(SalaryProject project, Account account, decimal salary)
    {
        var query = @"
            INSERT INTO Salary (AccountId, SalaryProjectId, Amount, Status)
            VALUES (@AccountId, @SalaryProjectId, @Amount, @Status)";

        var parameters = new Dictionary<string, object>
        {
            ["AccountId"] = account.Id,
            ["SalaryProjectId"] = project.Id,
            ["Amount"] = salary,
            ["Status"] = SalaryStatus.Active
        };

        await Task.Run(()=>_dataBaseHelper.ExecuteNonQuery(query, parameters));
    }

    public async Task<IEnumerable<SalaryProject>> GetSalaryProjectRequests()
    {
        var query = "SELECT * FROM SalaryProject WHERE Status = @Status";
        var parameters = new Dictionary<string, object>
        {
            ["Status"] = (int)SalaryProjectStatus.Application
        };

        var result = await Task.Run(()=>_dataBaseHelper.ExecuteQuery(query, parameters));

        var projects = new List<SalaryProject>();

        foreach (var row in result)
        {
            projects.Add(new SalaryProject
            {
                Id = Convert.ToInt32(row["Id"]),
                EnterpriseId = Convert.ToInt32(row["EnterpriseId"]),
                Balance = Convert.ToDecimal(row["Balance"]),
                BankId = Convert.ToInt32(row["BankId"]),
                Status = (SalaryProjectStatus)Convert.ToInt32(row["Status"])
            });
        }

        return projects;
    }


    public Task AddSalaryAsync(SalaryProject project, Account account, decimal salary)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateSalaryAsync(SalaryProject project, Account account, decimal amount)
    {
        var query = @"
            UPDATE Salary
            SET Amount = @Amount
            WHERE AccountId = @AccountId AND SalaryProjectId = @SalaryProjectId";

        var parameters = new Dictionary<string, object>
        {
            ["AccountId"] = account.Id,
            ["SalaryProjectId"] = project.Id,
            ["Amount"] = amount,
            ["Status"] = SalaryStatus.Active
        };

        await Task.Run(()=>_dataBaseHelper.ExecuteNonQuery(query, parameters));
    }

    public async Task UpdateSalaryAsync(Salary salary)
    {
        var query = @"
            UPDATE Salary
            SET AccountId = @AccountId, SalaryProjectId = @SalaryProjectId, Amount = @Amount, Status = @Status
            WHERE Id = @Id";

        var parameters = new Dictionary<string, object>
        {
            ["Id"] = salary.Id,
            ["AccountId"] = salary.AccountId,
            ["SalaryProjectId"] = salary.SalaryProjectId,
            ["Amount"] = salary.Amount,
            ["Status"] = (int)salary.Status
        };

        await Task.Run(() =>_dataBaseHelper.ExecuteNonQuery(query, parameters));
    }

    public async Task<Salary> GetSalaryAsync(int salaryId)
    {
        var query = "SELECT * FROM Salary " +
                    "WHERE Id = @salaryId and Status = @Status";
        var parameters = new Dictionary<string, object>
        {
            ["salaryId"] = salaryId,
            ["Status"] = (int)SalaryStatus.Active
        };

        var result = await Task.Run(()=>_dataBaseHelper.ExecuteQuery(query, parameters).FirstOrDefault());

        if (result == null)
        {
            return null;
        }

        return new Salary
        {
            Id = Convert.ToInt32(result["Id"]),
            AccountId = Convert.ToInt32(result["AccountId"]),
            SalaryProjectId = Convert.ToInt32(result["SalaryProjectId"]),
            Amount = Convert.ToDecimal(result["Amount"]),
            Status = (SalaryStatus)Convert.ToInt32(result["Status"])
        };
    }

    public async Task<IEnumerable<SalaryProject>> GetAllSalaryProjectAsync()
    {
        var query = "SELECT * FROM SalaryProject WHERE Status = @Status";
        var parameters = new Dictionary<string, object>
        {
            ["Status"] = (int)SalaryProjectStatus.Active
        };

        var result = await Task.Run(()=>_dataBaseHelper.ExecuteQuery(query, parameters));

        var projects = new List<SalaryProject>();

        foreach (var row in result)
        {
            projects.Add(new SalaryProject
            {
                Id = Convert.ToInt32(row["Id"]),
                EnterpriseId = Convert.ToInt32(row["EnterpriseId"]),
                Balance = Convert.ToDecimal(row["Balance"]),
                BankId = Convert.ToInt32(row["BankId"]),
                Status = (SalaryProjectStatus)Convert.ToInt32(row["Status"])
            });
        }

        return projects;
    }

    public async Task<IEnumerable<Salary>> GetAllSalaryRequests()
    {
        var query = "SELECT * FROM Salary " +
                    "WHERE Status = @Status";
        var parameters = new Dictionary<string, object>
        {
            ["Status"] = (int)SalaryStatus.Application
        };

        var results = await Task.Run(()=>_dataBaseHelper.ExecuteQuery(query, parameters));

        return results.Select(result => new Salary
        {
            Id = Convert.ToInt32(result["Id"]),
            AccountId = Convert.ToInt32(result["AccountId"]),
            SalaryProjectId = Convert.ToInt32(result["SalaryProjectId"]),
            Amount = Convert.ToDecimal(result["Amount"]),
            Status = (SalaryStatus)Convert.ToInt32(result["Status"])
        });
    }

    public async Task<Salary> GetSalaryRequest(int salaryId)
    {
        var query = "SELECT * FROM Salary " +
                    "WHERE Id = @salaryId and Status = @Status";
        var parameters = new Dictionary<string, object>
        {
            ["salaryId"] = salaryId,
            ["Status"] = (int)SalaryStatus.Application
        };

        var result = await Task.Run(()=>_dataBaseHelper.ExecuteQuery(query, parameters).FirstOrDefault());

        if (result == null)
        {
            return null;
        }

        return new Salary
        {
            Id = Convert.ToInt32(result["Id"]),
            AccountId = Convert.ToInt32(result["AccountId"]),
            SalaryProjectId = Convert.ToInt32(result["SalaryProjectId"]),
            Amount = Convert.ToDecimal(result["Amount"]),
            Status = (SalaryStatus)Convert.ToInt32(result["Status"])
        };
    }

    public async Task<IEnumerable<SalaryProject>> GetAllSalaryProjectRequests()
    {
        var query = "SELECT * FROM SalaryProject WHERE Status = @Status";
        var parameters = new Dictionary<string, object>
        {
            ["Status"] = (int)SalaryProjectStatus.Application
        };

        var result = await Task.Run(()=>_dataBaseHelper.ExecuteQuery(query, parameters));

        var projects = new List<SalaryProject>();

        foreach (var row in result)
        {
            projects.Add(new SalaryProject
            {
                Id = Convert.ToInt32(row["Id"]),
                EnterpriseId = Convert.ToInt32(row["EnterpriseId"]),
                Balance = Convert.ToDecimal(row["Balance"]),
                BankId = Convert.ToInt32(row["BankId"]),
                Status = (SalaryProjectStatus)Convert.ToInt32(row["Status"])
            });
        }

        return projects;
    }

    public async Task<SalaryProject> GetSalaryProjectRequest(int id)
    {
        var query = "SELECT * FROM SalaryProject WHERE Id = @Id AND Status = @Status";
        var parameters = new Dictionary<string, object>
        {
            ["Id"] = id,
            ["Status"] = (int)SalaryProjectStatus.Application
        };

        var result = await Task.Run(()=>_dataBaseHelper.ExecuteQuery(query, parameters).FirstOrDefault());

        if (result == null)
            return null;

        return new SalaryProject
        {
            Id = Convert.ToInt32(result["Id"]),
            EnterpriseId = Convert.ToInt32(result["EnterpriseId"]),
            Balance = Convert.ToDecimal(result["Balance"]),
            BankId = Convert.ToInt32(result["BankId"]),
            Status = (SalaryProjectStatus)Convert.ToInt32(result["Status"])
        };
    }

    public async Task<IEnumerable<Salary>> GetSalaries(int projectId)
    {
        var query = "SELECT * FROM Salary " +
            "WHERE SalaryProjectId = @SalaryProjectId and Status = @Status";
        var parameters = new Dictionary<string, object>
        {
            ["SalaryProjectId"] = projectId,
            ["Status"] = (int)SalaryStatus.Active
        };

        var results = await Task.Run(()=>_dataBaseHelper.ExecuteQuery(query, parameters));

        return results.Select(result => new Salary
        {
            Id = Convert.ToInt32(result["Id"]),
            AccountId = Convert.ToInt32(result["AccountId"]),
            SalaryProjectId = Convert.ToInt32(result["SalaryProjectId"]),
            Amount = Convert.ToDecimal(result["Amount"]),
            Status = (SalaryStatus)Convert.ToInt32(result["Status"])
        });
    }

    public async Task AddSalaryAsync(Salary salary)
    {
        var query = @"
        INSERT INTO Salary (AccountId, SalaryProjectId, Amount, Status)
        VALUES (@AccountId, @SalaryProjectId, @Amount, @Status)";

        var parameters = new Dictionary<string, object>
        {
            ["AccountId"] = salary.AccountId,
            ["SalaryProjectId"] = salary.SalaryProjectId,
            ["Amount"] = salary.Amount,
            ["Status"] = salary.Status
        };

        await Task.Run(()=>_dataBaseHelper.ExecuteNonQuery(query, parameters));
    }

    public async Task<IEnumerable<Salary>> GetSalaryRequests(int projectId)
    {
        var query = "SELECT * FROM Salary " +
                    "WHERE SalaryProjectId = @SalaryProjectId and Status = @Status";
        var parameters = new Dictionary<string, object>
        {
            ["SalaryProjectId"] = projectId,
            ["Status"] = (int)SalaryStatus.Application
        };

        var results = await Task.Run(()=>_dataBaseHelper.ExecuteQuery(query, parameters));

        return results.Select(result => new Salary
        {
            Id = Convert.ToInt32(result["Id"]),
            AccountId = Convert.ToInt32(result["AccountId"]),
            SalaryProjectId = Convert.ToInt32(result["SalaryProjectId"]),
            Amount = Convert.ToDecimal(result["Amount"]),
            Status = (SalaryStatus)Convert.ToInt32(result["Status"])
        });
    }
}