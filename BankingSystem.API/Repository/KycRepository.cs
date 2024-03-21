﻿using Microsoft.EntityFrameworkCore;
using BankingSystem.API.Models;
using BankingSystem.API.IRepository;
using Microsoft.AspNetCore.JsonPatch;
using BankingSystem.API.DTO;
using AutoMapper;

namespace RESTful_API__ASP.NET_Core.Repository
{
    public class KycRepository : IKycRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public KycRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<KycDocument> AddKycDocumentAsync(KycDocument kycDocument)
        {
            _context.KycDocument.Add(kycDocument);
            await _context.SaveChangesAsync();
            return kycDocument;
        }
        public async Task<KycDocument> UpdateKycDocumentAsync(Guid KYCId, KycDocument updatedKycDocument)
        {
            // Fetch the existing KYC document
            var existingKycDocument = await _context.KycDocument.FindAsync(KYCId);
            if (existingKycDocument != null)
            {
                existingKycDocument.FatherName = updatedKycDocument.FatherName;
                existingKycDocument.MotherName = updatedKycDocument.MotherName;
                existingKycDocument.GrandFatherName = updatedKycDocument.GrandFatherName;
                existingKycDocument.PermanentAddress = updatedKycDocument.PermanentAddress;
                existingKycDocument.UploadedAt = updatedKycDocument.UploadedAt;

                if (updatedKycDocument.UserImagePath != null)
                {
                    existingKycDocument.UserImagePath = updatedKycDocument.UserImagePath;
                }
                if (updatedKycDocument.CitizenshipImagePath != null)
                {
                    existingKycDocument.CitizenshipImagePath = updatedKycDocument.CitizenshipImagePath;
                }

                await _context.SaveChangesAsync();
            }
            return existingKycDocument;
        }

        //no need for all KYC docs at once
        public async Task<IEnumerable<KycDocument>> GetKycDocumentAsync()
        {
            return await _context.KycDocument.ToListAsync();
        }

        public async Task<KycDocument?> GetKYCIdAsync(Guid KYCId)
        {
            return await _context.KycDocument.FindAsync(KYCId);
        }

        public async Task<KycDocument> GetKycByUserIdAsync(Guid userId)
        {
            return await _context.KycDocument.Where(k => k.UserId == userId).FirstOrDefaultAsync();
        }


        public async Task<KycDocument> UpdateKycDocumentAsync(Guid KYCId, JsonPatchDocument<KycDocumentDTO> kycDetails)
        {
            var kycDocument = await GetKYCIdAsync(KYCId);
            if (kycDocument == null)
            {
                return null;
            }

            var kycDocumentDto = new KycDocumentDTO();
            kycDetails.ApplyTo(kycDocumentDto);

            _mapper.Map(kycDocumentDto, kycDocument);

            return await AddKycDocumentAsync(kycDocument);
        }
    }
}