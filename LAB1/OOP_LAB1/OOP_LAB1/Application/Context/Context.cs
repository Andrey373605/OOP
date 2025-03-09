using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Application.Context;

public class Context : IContext
{
    public User? CurrentUser { get; private set; }
    public Bank? CurrentBank { get; private set; }
    
    public void SetCurrent(User user)
    {
        CurrentUser = user;
    }

    public void SetCurrent(Bank bank)
    {
        CurrentBank = bank;
    }

    public void ClearCurrentUser()
    {
        CurrentUser = null;
    }

    public void ClearCurrentBank()
    {
        CurrentBank = null;
    }
}