using BankingSystem.API.DTO;
using BankingSystem.API.Models;

namespace BankingSystem.API.Services.IServices
{
    public interface IAccountService
    {
        Task<Accounts?> GetAccountAsync(Guid accountId);
        Task<IEnumerable<Accounts>> GetAccountsAsync();
        Task<Accounts?> GetAccountByUserIdAsync(Guid userId);
        Task<Accounts> AddAccounts(AccountDTO accounts , UserCreationDTO users) ;
        void DeleteAccount(Guid accountId);
        Task<Accounts> UpdateAccountsAsync(Guid accountId, AccountDTO accounts);
    }
}
