using AutoMapper;
using BankingSystem.API.DTO;
using BankingSystem.API.IRepository;
using BankingSystem.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using RESTful_API__ASP.NET_Core.Repository;
using System.Text;

namespace BankingSystem.API.Services
{
    public class TransactionServices
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IMapper _mapper;
        public TransactionServices(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository ?? throw new ArgumentOutOfRangeException(nameof(transactionRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<Transaction>> GetTransactionsOfAccountAsync(Guid accountId)
        {
            return await _transactionRepository.GetTransactionsOfAccountAsync(accountId);
        }

        public void DeleteTransaction(Guid accountId, Guid transactionId)
        {
            _transactionRepository.DeleteTransaction(accountId, transactionId);
        }

        public async Task<bool> TransactionExistAsync(Guid transactionId)
        {
            return await _transactionRepository.TransactionExistAsync(transactionId);
        }

        public async Task<bool> IsVerifiedKycAsync(Guid kycId)
        {
            return await _transactionRepository.IsVerifiedKycAsync(kycId);
        }

        public async Task<Transaction> DepositTransactionAsync(DepositTransactionDTO transactionDto, Guid accountId)
        {
            var transaction = _mapper.Map<Transaction>(transactionDto);
            return await _transactionRepository.DepositTransactionAsync(transaction, accountId);
        }

    }
}
