using BankingSystem.API.DTOs;
using BankingSystem.API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BankingSystem.API.Services.IServices
{
    public interface IKycService
    {
        Task<IEnumerable<KycDocument>> GetKycDocumentAsync();
        Task<KycDocument?> GetKycDocumentAsync(Guid KYCId);
        Task<KycDocument> GetKycByUserIdAsync(Guid Id);
        Task<KycDocument> AddKycDocumentAsync(KycDocumentDTO kycDocumentDto);
        Task<KycDocument> UpdateKycDocumentAsync(Guid KYCId, KycDocumentDTO updatedKycDocumentDto);
    }
}
