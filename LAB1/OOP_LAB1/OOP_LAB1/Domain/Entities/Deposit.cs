namespace OOP_LAB1.Domain.Entities;

public class Deposit
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public decimal Amount { get; set; }
    public decimal InterestRate { get; set; }
    public int MonthCount { get; }
}