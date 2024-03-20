using BankingSystem.API.DTO;
using BankingSystem.API.Models;

namespace BankingSystem.API.IRepository
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetTransactionsOfAccountAsync(Guid accountId);

        Task DeleteTransaction(Guid accountId, Guid transactionId);

        Task<bool> TransactionExistAsync(Guid transactionId);

        Task<Transaction?> GetTransactionFromAccountAsync(Guid accountId, Guid transactionId);

        Task<bool> IsVerifiedKycAsync(Guid kycId);

        Task<Transaction> DepositTransactionAsync(Transaction transaction, Guid accountId);

        Task<Transaction> WithdrawTransactionAsync(Transaction transaction, Guid accountId, int atmIdAtmCardPin);
    }
}
