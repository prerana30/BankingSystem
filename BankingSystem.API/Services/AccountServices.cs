using AutoMapper;
using BankingSystem.API.Data.Repository.IRepository;
using BankingSystem.API.DTO;
using BankingSystem.API.Models;
using BankingSystem.API.Services.IServices;



namespace BankingSystem.API.Services
{
    public class AccountServices: IAccountService
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

        public async Task<Accounts> UpdateAccountsAsync(Guid accountId, AccountDTO accounts)
        {
            var finalAccount = _mapper.Map<Accounts>(accounts);
            return await AccountRepository.UpdateAccountsAsync(accountId, finalAccount);
        }
    }
}