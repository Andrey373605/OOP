using OOP_LAB1.Domain.Entities;
namespace OOP_LAB1.Application.Context;


public class UserContext
{
    public User CurrentUser { get; private set; }

    public void SetCurrent(User user)
    {
        CurrentUser = user;
    }

    public void ClearCurrent()
    {
        CurrentUser = null;
    }
}


