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



        public async Task<IEnumerable<Transaction>> GetTransactionsOfAccountAsync(int accountId)
        {
            return await _context.Transaction
                .Where(p => p.AccountId == accountId)
                .ToArrayAsync();
        }


        public async Task<Transaction?> GetTransactionFromAccountAsync(int accountId, int transactionId)
        {
            return await _context.Transaction
                .Where(p => p.AccountId == accountId && p.TransactionId == transactionId)
                .FirstOrDefaultAsync();
        }

        public async Task DeleteTransaction(int accountId, int transactionId)
        {
            var delTransaction = await GetTransactionFromAccountAsync(accountId, transactionId);
            if (delTransaction != null)
            {
                _context.Transaction.Remove(delTransaction);
                await _context.SaveChangesAsync();
            }
        }



        public async Task<bool> TransactionExistAsync(int transactionId)
        {
            return await _context.Transaction.AnyAsync(c => c.TransactionId == transactionId);
        }
    }
}
