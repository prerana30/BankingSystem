using AutoMapper;
using BankingSystem.API.DTO;
using BankingSystem.API.Models;

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

