using AutoMapper;
using BankingSystem.API.DTO;
using BankingSystem.API.Models;

namespace BankingSystem.API.Profiles
{
    public class UserProfile: Profile
    {
        public UserProfile() {
            CreateMap<Users, UserDTO>(); //from entity to dto(get)
            CreateMap<UserDTO, Users>(); //from dto to entity (post)

            CreateMap<Accounts, AccountDTO>();
            CreateMap<AccountDTO, Accounts>();

            CreateMap<KycDocument, KycDocumentDTO>();
            CreateMap<KycDocumentDTO, KycDocument>();
        }
    }
}
