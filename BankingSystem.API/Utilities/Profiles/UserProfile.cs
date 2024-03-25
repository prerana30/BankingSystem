using AutoMapper;
using BankingSystem.API.DTOs;
using BankingSystem.API.Entities;

namespace BankingSystem.API.Utilities.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<Users, UserCreationDTO>(); //from entity to dto(get)
            CreateMap<Users, UserInfoDisplayDTO>(); //from entity to dto(get)

            CreateMap<UserCreationDTO, Users>(); //from dto to entity (post)
            CreateMap<UserUpdateDTO, Users>(); //from dto to entity (put)
        }
    }
}
