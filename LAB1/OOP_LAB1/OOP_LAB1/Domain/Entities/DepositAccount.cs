namespace OOP_LAB1.Domain.Entities;

public class DepositAccount
{
    public int Id { get; set; }
    public decimal Balance { get; set; }
    public int OwnerId { get; set; }
}