namespace OOP_LAB1.Domain.Entities;

public class EnterpriseAccount
{
    public int Id { get; set; }
    private decimal Balance { get; set; }
    
    public int OwnerId { get; set; }
    public bool IsBlocked { get; set; }
    public bool IsFrozen { get; set; }
    
    public void FreezeAccount()
    {
        IsFrozen = true;
    }
        
    public void UnfreezeAccount()
    {
        IsFrozen = false;
    }

    public void BlockAccount()
    {
        IsBlocked = true;
    }

    public void UnblockAccount()
    {
        IsBlocked = false;
    }

    public void DepositAccount(decimal amount)
    {
        Balance += amount;
    }

    public void WithdrawAccount(decimal amount)
    {
        Balance -= amount;
    }
}