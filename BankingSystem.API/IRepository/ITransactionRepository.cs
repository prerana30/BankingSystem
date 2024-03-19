﻿using BankingSystem.API.Models;

namespace BankingSystem.API.IRepository
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetTransactionsOfAccountAsync(Guid accountId);

        Task DeleteTransaction(Guid accountId, Guid transactionId);

        Task<bool> TransactionExistAsync(Guid transactionId);

        Task<Transaction?> GetTransactionFromAccountAsync(Guid accountId, Guid transactionId);
    }
}
