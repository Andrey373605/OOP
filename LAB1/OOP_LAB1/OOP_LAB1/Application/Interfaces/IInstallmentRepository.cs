using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Application.Interfaces;

public interface IInstallmentRepository
{
    public Task AddAsync(Installment installment);
    
    public Task UpdateAsync(Installment installment);
    
    public Task DeleteAsync(Installment installment);
    public Task<Installment> GetByIdAsync(int installmentId);
}