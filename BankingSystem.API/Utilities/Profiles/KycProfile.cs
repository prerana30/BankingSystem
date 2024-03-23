using AutoMapper;
using BankingSystem.API.DTO;
using BankingSystem.API.Models;

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
