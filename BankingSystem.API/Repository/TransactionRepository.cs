using BankingSystem.API.DTO;
using BankingSystem.API.IRepository;
using BankingSystem.API.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.API.Repository
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly ApplicationDbContext _context;


        public TransactionRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentOutOfRangeException(nameof(context));
        }



        public async Task<IEnumerable<Transaction>> GetTransactionsOfAccountAsync(Guid accountId)
        {
            return await _context.Transaction
                .Where(p => p.AccountId == accountId)
                .ToArrayAsync();
        }


        public async Task<Transaction?> GetTransactionFromAccountAsync(Guid accountId, Guid transactionId)
        {
            return await _context.Transaction
                .Where(p => p.AccountId == accountId && p.TransactionId == transactionId)
                .FirstOrDefaultAsync();
        }

        public async Task DeleteTransaction(Guid accountId, Guid transactionId)
        {
            var delTransaction = await GetTransactionFromAccountAsync(accountId, transactionId);
            if (delTransaction != null)
            {
                _context.Transaction.Remove(delTransaction);
                await _context.SaveChangesAsync();
            }
        }


        public async Task<bool> TransactionExistAsync(Guid transactionId)
        {
            return await _context.Transaction.AnyAsync(c => c.TransactionId == transactionId);
        }


        public async Task<bool> IsVerifiedKycAsync(Guid kycId)
        {
            return await _context.KycDocument.AnyAsync(c => c.KYCId == kycId);
        }

        //public async Task<Transaction> DepositTransactionAsync(Transaction transaction, Guid accountId)
        //{
        //    var account= await _context.Accounts.FirstOrDefaultAsync(c => c.AccountId == accountId);
        //    //var kycAccount= await _context.KycDocument.FirstOrDefaultAsync(c => c.  Id == account.User.  Id);
        //    var kycAccount = await _context.KycDocument.FirstOrDefaultAsync(c => c.  Id == account.  Id);

        //    var isVerified = await IsVerifiedKycAsync(kycAccount.KYCId);


        //    if (isVerified)
        //    {
        //        _context.Transaction.Add(transaction);
        //        await _context.SaveChangesAsync();
        //        return transaction;
        //    }
        //    else
        //    {
        //        throw new Exception("KYC is not verified, transaction cannot be made.");
        //    }
        //}



        public async Task<Transaction> DepositTransactionAsync(Transaction transaction, Guid accountId)
        {
            var account = await _context.Accounts.FirstOrDefaultAsync(c => c.AccountId == accountId);

            if (account is null)
            {
                throw new Exception($"Account with  Id{accountId} not found.");
            }

            var kycAccount = await _context.KycDocument.FirstOrDefaultAsync(c => c.  Id == account.  Id);

            if (kycAccount is null)
            {
                throw new Exception($"KYC document not found for user  Id{account.User.  Id}.");
            }

            var isVerified = await IsVerifiedKycAsync(kycAccount.KYCId);

            if (isVerified is true)
            {
                // Set the accountId for the transaction
                transaction.AccountId = accountId;

                _context.Transaction.Add(transaction);
                await _context.SaveChangesAsync();
                return transaction;
            }
            else
            {
                throw new Exception("KYC is not verified, transaction cannot be made.");
            }
        }

    }
}
