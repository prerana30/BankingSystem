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

            //returns only user detail
            //return await _transactionRepository.GetTransactionsOfAccountAsync(accountId);
            //if (!await _transactionRepository.TransactionExistAsync(accountId))
            //{
            //    _logger.LogInformation($"Account with id {accountId} don't have any transaction.");
            //    return NotFound();
            //}
            //var transactionsOfAccount = await _transactionRepository
            //    .GetTransactionsOfAccountAsync(accountId);
            //return _mapper.Map<IEnumerable<TransactionDTO>>(transactionsOfAccount);
        }


        public void DeleteTransaction(Guid accountId, Guid transactionId)
        {
            _transactionRepository.DeleteTransaction(accountId, transactionId);
        }


        public async Task<bool> TransactionExistAsync(Guid transactionId)
        {
            return await _transactionRepository.TransactionExistAsync(transactionId);
        }
    }
}
