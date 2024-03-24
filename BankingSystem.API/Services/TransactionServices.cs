﻿using AutoMapper;
using BankingSystem.API.Data.Repository.IRepository;
using BankingSystem.API.DTO;
using BankingSystem.API.Models;
using BankingSystem.API.Services.IServices;

namespace BankingSystem.API.Services
{
    public class TransactionServices: ITransactionService
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

        public async Task<Transaction> DepositTransactionAsync(DepositTransactionDTO transactionDto, Guid accountId, Guid userId)
        {
            var transaction = _mapper.Map<Transaction>(transactionDto);
            return await _transactionRepository.DepositTransactionAsync(transaction, accountId, userId);
        }

        public async Task<Transaction> WithdrawTransactionAsync(WithdrawTransactionDTO withdrawDto, Guid accountId, int atmIdAtmCardPin)
        {
            var transaction = _mapper.Map<Transaction>(withdrawDto);
            return await _transactionRepository.WithdrawTransactionAsync(transaction, accountId, atmIdAtmCardPin);
        }
    }
}
