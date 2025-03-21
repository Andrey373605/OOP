using OOP_LAB1.Domain.Enums;

namespace OOP_LAB1.Domain.Entities;

public class ReplenishmentRequest
{
    public int Id { get; set; }
    public int SalaryProjectId { get; set; }
    public decimal Amount { get; set; }
    public RequestStatus Status { get; set; } = RequestStatus.Application;

    public void Approve()
    {
        Status = RequestStatus.Approved;
    }

    public void Reject()
    {
        Status = RequestStatus.Rejected;
    }
}