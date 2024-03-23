using BankingSystem.API.DTO;
using BankingSystem.API.IRepository;
using BankingSystem.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;
using RESTful_API__ASP.NET_Core.Repository;
using BankingSystem.API.Services;



namespace BankingSystem.API.Services
{
    public class AccountServices
    {
        private readonly IAccountRepository AccountRepository;
        private readonly EmailService _emailService;
       
        private readonly IMapper _mapper;
        public AccountServices(IAccountRepository accountRepository, EmailService emailService, IMapper mapper)
        {
            AccountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _emailService = emailService ?? throw new ArgumentNullException(nameof(emailService));
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

        /*  public async Task<Accounts> AddAccounts(UserDTO userDTO)
          {
              var finalAccount = _mapper.Map<Accounts>(userDTO);
              var addedAccount = await AccountRepository.AddAccounts(finalAccount);

              // Send email after adding the account
              var email = new Email
              {
                  MailSubject = "Welcome to Our Banking System",
                  MailBody = "Thank you for creating an account with us. Welcome aboard!",
                  ReceiverEmail = userDTO.Email // Use the email address from the UserDTO
              };

              await _emailService.SendEmailAsync(email);

              return addedAccount;
          }
  */
        public async Task<Accounts> AddAccounts(AccountDTO accounts)
        {
            var finalAccount = _mapper.Map<Accounts>(accounts);
            // finalAccount.AccountNumber = AccountNumberGenerator.GenerateAccountNumber();
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