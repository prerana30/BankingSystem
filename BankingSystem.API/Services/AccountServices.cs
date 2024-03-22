using BankingSystem.API.DTO;
using BankingSystem.API.IRepository;
using BankingSystem.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;
using RESTful_API__ASP.NET_Core.Repository;



namespace BankingSystem.API.Services
{
    public class AccountServices
    {
        private readonly IAccountRepository AccountRepository;

        private readonly IMapper _mapper;
        public AccountServices(IAccountRepository accountRepository, IMapper mapper)
        {
            AccountRepository = accountRepository ?? throw new ArgumentOutOfRangeException(nameof(accountRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Accounts?> GetAccountAsync(Guid accountId)
        {
            //returns only user detail
            return await AccountRepository.GetAccountAsync(accountId);
        }


        public async Task<IEnumerable<Accounts>> GetAccountsAsync()
        {
            return await AccountRepository.GetAccountsAsync();
        }
        public async Task<Accounts?> GetAccountByAccountNumberAsync(long accountNumber)
        {
            return await AccountRepository.GetAccountByAccountNumberAsync(accountNumber);
        }  

        public async Task<Accounts?> GetAccountByUserIdAsync(Guid userId)
        {
            return await AccountRepository.GetAccountByUserIdAsync(userId);
        }


        public async Task<Accounts> AddAccounts(AccountDTO accounts)
        {
            var finalAccount = _mapper.Map<Accounts>(accounts);
            return await AccountRepository.AddAccounts(finalAccount);
        }

        public void DeleteAccount(Guid accountId)
        {
            AccountRepository.DeleteAccount(accountId);
        }

      /*  public async Task<Accounts> PatchAccountDetails(Guid accountId, JsonPatchDocument<AccountDTO> patchDocument)
        {
            return await AccountRepository.PatchAccountDetails(accountId, patchDocument);
        }
*/
        public async Task<Accounts> UpdateAccountsAsync(Guid accountId, AccountDTO accounts)
        {
            var finalAccount = _mapper.Map<Accounts>(accounts);
            //string hashedPassword = BCrypt.Net.BCrypt.HashPassword(users.Password);
            //final.Password = hashedPassword;

            return await AccountRepository.UpdateAccountsAsync(accountId, finalAccount);
        }
    }
}