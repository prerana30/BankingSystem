using BankingSystem.API.DTOs;
using BankingSystem.API.Entities;

namespace BankingSystem.API.Services.IServices
{
    public interface IAccountService
    {
        Task<Accounts?> GetAccountAsync(Guid accountId);
        Task<IEnumerable<Accounts>> GetAccountsAsync();
        Task<Accounts?> GetAccountByUserIdAsync(Guid userId);
        Task<Accounts> AddAccounts(Accounts accounts , string useremail) ;
        void DeleteAccount(Guid accountId);
        Task<Accounts> UpdateAccountsAsync(Guid accountId, Accounts accounts);
    }
}
