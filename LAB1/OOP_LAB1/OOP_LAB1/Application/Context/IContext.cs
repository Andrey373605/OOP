using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Application.Context;

public interface IContext
{
    public User? CurrentUser { get; }
    public Bank? CurrentBank { get; }

    public void SetCurrent(User user);

    public void SetCurrent(Bank bank);

    public void ClearCurrentUser();

    public void ClearCurrentBank();
}