﻿using OOP_LAB1.Domain.Entities;

namespace OOP_LAB1.Application.Interfaces;

public interface ITransactionRepository
{
    public Task<Transaction> GetByIdAsync(int transactionId);

    public Task AddAsync(Transaction transaction);
    
    public Task UpdateAsync(Transaction transaction);
}