using BankingSystem.API.DTOs;
using BankingSystem.API.Entities;

namespace BankingSystem.API.Data.Repository.IRepository
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetTransactionsOfAccountAsync(long accountNumber);

        Task DeleteTransaction(Guid accountId, Guid transactionId);

        Task<bool> TransactionExistAsync(Guid transactionId);

        Task<Transaction?> GetTransactionFromAccountAsync(Guid accountId, Guid transactionId);

        Task<bool> IsVerifiedKycAsync(Guid kycId);

        Task<Transaction> DepositTransactionAsync(Transaction transaction, long accountNumber, Guid userId);

        Task<Transaction> WithdrawTransactionAsync(Transaction transaction, long accountNumber, int atmIdAtmCardPin);
    }
}
