﻿using System.Collections;
using OOP_LAB1.Domain.Entities;
namespace OOP_LAB1.Application.Interfaces;

public interface ILoanRepository
{
    public Task AddAsync(Loan loan);
    
    public Task UpdateAsync(Loan loan);
    
    public Task DeleteAsync(Loan loan);
    public Task<Loan> GetByIdAsync(int loanId);
    Task<IEnumerable<Loan>> GetAllByClientId(int clientId);
    Task<IEnumerable<Loan>> GetLoanApplications();
    Task<IEnumerable<Loan>> GetAllActiveLoans();
}