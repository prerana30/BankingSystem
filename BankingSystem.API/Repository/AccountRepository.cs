using BankingSystem.API.DTO;
using BankingSystem.API.IRepository;
using BankingSystem.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.API.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext _context;
        public AccountRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentOutOfRangeException(nameof(context));
        }
        public async Task<Accounts?> GetAccountAsync(Guid accountId)
        {
            //returns only account detail
            return await _context.Accounts.Where(a => a.AccountId == accountId).FirstOrDefaultAsync();
        }
        public async Task<IEnumerable<Accounts>> GetAccountsAsync()
        {
            //return await _context.Users.OrderBy(c => c.Name).ToListAsync();
            return await _context.Accounts.OrderBy(a => a.AccountNumber).ToListAsync();
        }

        async Task<Accounts?> GetAccountByAccountNumberAsync(long accountNumber)
        {
            return await _context.Accounts.Where(a => a.AccountNumber == accountNumber).FirstOrDefaultAsync();
        }
        public async Task<Accounts?> GetAccountByUserIdAsync(Guid userId)
        {
            return await _context.Accounts.Where(a => a.UserId == userId).FirstOrDefaultAsync();
        }


        public async Task<Accounts> AddAccounts(Accounts accounts)
        {
            var account = _context.Accounts.Add(accounts);
            await _context.SaveChangesAsync();

            return GetAccountAsync(account.Entity.AccountId).Result;
        }

        public void DeleteAccount(Guid accountId)
        {
            var account = GetAccountAsync(accountId);
            _context.Accounts.Remove(account.Result);
            _context.SaveChangesAsync();
        }



        public async Task<Accounts> PatchAccountDetails(Guid accountId, JsonPatchDocument<AccountDTO> aDetails)
        {
            var existingAccount = await GetAccountAsync(accountId);
            if (existingAccount != null)
            {
                var accountToPatch = new AccountDTO(existingAccount.UserId,existingAccount.Balance, existingAccount.AtmCardNum, existingAccount.AtmCardPin, existingAccount.AccountCreatedAt, existingAccount.AccountCreatedBy, existingAccount.AccountModifiedAt, existingAccount.AccountModifiedBy);
                 aDetails.ApplyTo(accountToPatch);
                existingAccount.Balance = accountToPatch.Balance;
                existingAccount.AtmCardNum = accountToPatch.AtmCardNum;
                existingAccount.AtmCardPin = accountToPatch.AtmCardPin;
/*                string hashedCardNum = BCrypt.Net.BCrypt.HashPassword(accountToPatch.AtmCardNum);
                existingAccount.AtmCardNum = hashedCardNum;
                string hashedCardPin = BCrypt.Net.BCrypt.HashPassword(accountToPatch.AtmCardPin);
                existingAccount.AtmCardNum = hashedCardPin;*/
               



                _context.SaveChanges();
                return existingAccount;
            }
            return null;
        }

        
        //{
        //     existingUser = await GetUserAsync(userId);
        //    if (existingUser != null)
        //    {
        //        //transform user entity to usercreationDTO
        //        var userToPatch = new UserDTO(existingUser.Username, existingUser.Fullname, existingUser.Email, existingUser.Password, existingUser.Address, existingUser.UserType, existingUser.DateOfBirth, existingUser.CreatedAt);

        //        patchDocument.ApplyTo(userToPatch);

        //        existingUser.Username = userToPatch.Username;
        //        existingUser.Fullname = userToPatch.Fullname;
        //        existingUser.Email = userToPatch.Email;

        //        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userToPatch.Password);

        //        existingUser.Password = hashedPassword;
        //        existingUser.Address = userToPatch.Address;
        //        existingUser.UserType = userToPatch.UserType;
        //        existingUser.DateOfBirth = userToPatch.DateOfBirth;
        //        existingUser.CreatedAt = userToPatch.CreatedAt;

        //        _context.SaveChanges();
        //        return existingUser;
        //    }
        //    return null;
        //}
        public async Task<Accounts> UpdateAccountsAsync(Guid accountId, Accounts finalaccounts)
        {
            var existingAccount = await GetAccountAsync(accountId);
            if (existingAccount != null)
            {
                existingAccount.Balance = finalaccounts.Balance;
                existingAccount.AtmCardNum = finalaccounts.AtmCardNum;
                existingAccount.AtmCardPin = finalaccounts.AtmCardPin;

                _context.SaveChanges();
                return existingAccount;
            }
            return null;
        }

        Task<Accounts?> IAccountRepository.GetAccountByAccountNumberAsync(long accountNumber)
        {
            throw new NotImplementedException();
        }

        public Task<Accounts> AddAccounts(Users users)
        {
            throw new NotImplementedException();
        }

        public Task<Accounts> UpdateAccountAsync(Guid accountId, object finalUser)
        {
            throw new NotImplementedException();
        }
    }
}







