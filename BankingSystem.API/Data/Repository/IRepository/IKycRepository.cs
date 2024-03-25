using BankingSystem.API.DTOs;
using BankingSystem.API.Entities;
using Microsoft.AspNetCore.JsonPatch;

namespace BankingSystem.API.Data.Repository.IRepository
{
    public interface IKycRepository
    {
        Task<IEnumerable<KycDocument>> GetKycDocumentAsync();
        Task<KycDocument?> GetKYCIdAsync(Guid KYCId);
        Task<KycDocument> GetKycByUserIdAsync(Guid Id);
        Task<KycDocument> AddKycDocumentAsync(KycDocument kycDocument);
        Task<KycDocument> UpdateKycDocumentAsync(Guid KYCId, KycDocument kycDocument);
        public Task<KycDocument> UpdateKycDocumentAsync(Guid KYCId, JsonPatchDocument<KycDocumentDTO> kycDetails);
    }
}
