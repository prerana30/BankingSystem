using AutoMapper;
using BankingSystem.API.DTOs;
using BankingSystem.API.Entities;

namespace BankingSystem.API.Utilities.Profiles
{
    public class KycProfile : Profile
    {
        public KycProfile()
        {
            CreateMap<KycDocument, KycDocumentDTO>();
            CreateMap<KycDocumentDTO, KycDocument>();
        }
    }
}
