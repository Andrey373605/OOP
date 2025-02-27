namespace OOP_LAB1.Domain.Entities;

public class DepositRequest
{
    public int Id { get; set; }
    public int UserId { get; set; }

    public int MonthCount { get; set; }
    public decimal Amount { get; set; }
    public decimal InterestRate { get; set; }
}