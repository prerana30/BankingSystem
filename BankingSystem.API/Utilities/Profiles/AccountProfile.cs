using AutoMapper;
using BankingSystem.API.DTOs;
using BankingSystem.API.Entities;

namespace BankingSystem.API.Utilities.Profiles
{
    public class AccountProfile : Profile
    {
        public AccountProfile()
        {
            CreateMap<Accounts, AccountDTO>();
            CreateMap<AccountDTO, Accounts>();

            CreateMap<AccountUpdateDTO, Accounts>(); 
        }
    }
}
