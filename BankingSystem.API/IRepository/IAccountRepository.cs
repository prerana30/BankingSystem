using BankingSystem.API.DTO;
using BankingSystem.API.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace BankingSystem.API.IRepository
{
    public interface IAccountRepository
    {
        Task<IEnumerable<Accounts>> GetAccountsAsync();
        Task<Accounts?> GetAccountAsync(int accountId);
        Task<Accounts?> GetAccountByAccountNumberAsync(long accountNumber);
        Task<Accounts> AddAccounts(Accounts accounts);
        Task<Accounts> UpdateAccountsAsync(int accountId, Accounts accounts);
        void DeleteAccount(int accountId);
        Task<Accounts> PatchAccountDetails(int accountId, JsonPatchDocument<AccountDTO> aDetails);
        Task<Accounts> UpdateAccountAsync(int accountId, object finalAccount);
    }
}
