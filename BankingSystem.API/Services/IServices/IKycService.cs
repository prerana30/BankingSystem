﻿using BankingSystem.API.DTO;
using BankingSystem.API.Models;

namespace BankingSystem.API.Services.IServices
{
    public interface IKYCService
    {
        Task<KycDocument?> GetKycDocumentAsync(Guid KYCId);
        Task<KycDocument> GetKycByUserIdAsync(Guid Id);
        Task<KycDocument> AddKycDocumentAsync(KycDocumentDTO kycDocumentDto);
        Task<KycDocument> UpdateKycDocumentAsync(Guid KYCId, KycDocumentDTO updatedKycDocumentDto);
    }
}