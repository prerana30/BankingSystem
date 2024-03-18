using BankingSystem.API.Models;

namespace BankingSystem.API.IRepository
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetTransactionsOfAccountAsync(int accountId);

        Task DeleteTransaction(int accountId, int transactionId);

        Task<bool> TransactionExistAsync(int transactionId);

        Task<Transaction?> GetTransactionFromAccountAsync(int accountId, int transactionId);
    }
}
