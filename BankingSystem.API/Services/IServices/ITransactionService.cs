using BankingSystem.API.DTOs;
using BankingSystem.API.Entities;

namespace BankingSystem.API.Services.IServices
{
    public interface ITransactionService
    {
        Task<IEnumerable<Transaction>> GetTransactionsOfAccountAsync(long accountNumber);
        Task<Transaction> DepositTransactionAsync(DepositTransactionDTO transactionDto, long accountNumber, Guid userId);
        Task<Transaction> WithdrawTransactionAsync(WithdrawTransactionDTO withdrawDto, long accountNumber, int atmIdAtmCardPin);
    }
}
