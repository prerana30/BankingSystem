using BankingSystem.API.DTO;
using BankingSystem.API.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace BankingSystem.API.IRepository
{
    public interface IKycRepository
    {
        Task<IEnumerable<KycDocument>> GetKycDocumentAsync();
        Task<KycDocument?> GetKYCIdAsync(Guid KYCId);
        Task<KycDocument> GetKycByUserIdAsync(Guid userId);
        Task<KycDocument> AddKycDocumentAsync(KycDocument kycDocument);
        Task<KycDocument> UpdateKycDocumentAsync(Guid KYCId, KycDocument kycDocument);
        void DeleteKycDocumentAsync(Guid KYCId);
        public Task<KycDocument> UpdateKycDocumentAsync(Guid KYCId, JsonPatchDocument<KycDocumentDTO> kycDetails);

    }
}
