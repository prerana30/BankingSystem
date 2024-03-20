using BankingSystem.API.DTO;
using BankingSystem.API.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace BankingSystem.API.IRepository
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Accounts>> GetAccountsAsync();
        Task<Accounts?> GetAccountAsync(Guid accountId);
        Task<Accounts?> GetAccountByAccountNumberAsync(long accountNumber);
        Task<Accounts?> GetAccountByUserIdAsync(Guid userId);
        Task<Accounts> AddAccounts(Accounts accounts);
        Task<Accounts> UpdateAccountsAsync(Guid accountId, Accounts accounts);
        void DeleteAccount(Guid accountId);
        Task<Accounts> PatchAccountDetails(Guid accountId, JsonPatchDocument<AccountDTO> aDetails);
        Task<Accounts> UpdateAccountAsync(Guid accountId, object finalAccount);
    }
}
