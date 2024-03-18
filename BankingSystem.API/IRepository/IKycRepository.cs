using BankingSystem.API.DTO;
using BankingSystem.API.Models;
using Microsoft.AspNetCore.JsonPatch;

namespace BankingSystem.API.IRepository
{
    public interface IKycRepository
    {
        Task<IEnumerable<KycDocument>> GetKycDocumentAsync();
        Task<KycDocument?> GetKYCIdAsync(int KYCId);
        Task<KycDocument> GetKycByUserIdAsync(int userId);
        Task<KycDocument> AddKycDocumentAsync(KycDocument kycDocument);
        Task<KycDocument> UpdateKycDocumentAsync(int KYCId, KycDocument kycDocument);
        void DeleteKycDocumentAsync(int KYCId);
        public Task<KycDocument> UpdateKycDocumentAsync(int KYCId, JsonPatchDocument<KycDocumentDTO> kycDetails);

    }
}
