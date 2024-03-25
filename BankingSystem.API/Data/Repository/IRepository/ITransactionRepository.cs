using BankingSystem.API.DTOs;
using BankingSystem.API.Entities;

namespace BankingSystem.API.Data.Repository.IRepository
{
    public interface ITransactionRepository
    {
        Task<IEnumerable<Transaction>> GetTransactionsOfAccountAsync(Guid accountId);

        Task DeleteTransaction(Guid accountId, Guid transactionId);

        Task<bool> TransactionExistAsync(Guid transactionId);

        Task<Transaction?> GetTransactionFromAccountAsync(Guid accountId, Guid transactionId);

        Task<bool> IsVerifiedKycAsync(Guid kycId);

        Task<Transaction> DepositTransactionAsync(Transaction transaction, Guid accountId, Guid userId);
        Task<Transaction> TellerDepositTransactionAsync(Transaction transaction, long accountNumber, Guid userId);

        Task<Transaction> WithdrawTransactionAsync(Transaction transaction, Guid accountId, int atmIdAtmCardPin);
        Task<Transaction> SelfWithdrawTransactionAsync(Transaction transaction, long accountNumber, int atmIdAtmCardPin);
    }
}
