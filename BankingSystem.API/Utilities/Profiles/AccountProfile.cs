using AutoMapper;
using BankingSystem.API.DTO;
using BankingSystem.API.Models;

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
