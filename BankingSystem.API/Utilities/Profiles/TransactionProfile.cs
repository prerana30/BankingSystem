using AutoMapper;
using BankingSystem.API.DTOs;
using BankingSystem.API.Entities;

namespace BankingSystem.API.Utilities.Profiles
{
    public class TransactionProfile : Profile
    {
        public TransactionProfile()
        {
            CreateMap<Transaction, TransactionDTO>();
            CreateMap<TransactionDTO, Transaction>();

            CreateMap<DepositTransactionDTO, Transaction>();
            CreateMap<WithdrawTransactionDTO, Transaction>();
        }
    }
}

