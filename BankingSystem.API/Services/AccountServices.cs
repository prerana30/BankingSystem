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



        /*   public async Task<Accounts> AddAccounts(AccountDTO accounts)
           {
               var finalAccount = _mapper.Map<Accounts>(accounts);
               // finalAccount.AccountNumber = AccountNumberGenerator.GenerateAccountNumber();
               return await AccountRepository.AddAccounts(finalAccount);
           }*/

        public async Task<Accounts> AddAccounts(AccountDTO accounts, UserCreationDTO users)
        {
            var finalAccount = _mapper.Map<Accounts>(accounts);
            var addedAccount = await AccountRepository.AddAccounts(finalAccount);

            // Obtain necessary information such as user's email address from the UserDTO object
            var userEmail = users.Email; // Assuming Email property is available in UserDTO


            var emailBody = "Dear user,<br><br>Your bank account has been successfully registered.<br><br>" +
                "Account number: " + accounts.AccountNumber + "<br>" +
                "ATM number: " + accounts.AtmCardNum + "<br>" +
                "ATM PIN: " + accounts.AtmCardPin + "<br><br>" +
                "Thank you for choosing our banking services. If you have any questions or need assistance, feel free to contact our support team.";
            // Prepare email
            var email = new Email
            {
                MailSubject = "Bank account registration Successful",
                MailBody = emailBody,
           
                ReceiverEmail = userEmail // Use the user's email address obtained from the UserDTO
            };

            // Send email
            await _emailService.SendEmailAsync(email);

            return addedAccount;
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