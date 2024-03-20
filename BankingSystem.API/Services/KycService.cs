using BankingSystem.API.DTO;
using BankingSystem.API.IRepository;
using BankingSystem.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;
using BankingSystem.API.Utils;
using System.Diagnostics;

namespace BankingSystem.API.Services
{
    public class KycService
    {
        private readonly IKycRepository _kycRepository;
        private readonly IMapper _mapper;
        private readonly FirebaseStorageHelper _firebaseStorageHelper;

        public KycService(IKycRepository kycRepository, IMapper mapper, FirebaseStorageHelper firebaseStorageHelper)
        {
            _kycRepository = kycRepository;
            _mapper = mapper;
            _firebaseStorageHelper = firebaseStorageHelper;
        }

        public async Task<IEnumerable<KycDocument>> GetKycDocumentAsync()
        {
            return await _kycRepository.GetKycDocumentAsync();
        }

        public async Task<KycDocument?> GetKycDocumentAsync(Guid KYCId)
        {
            return await _kycRepository.GetKYCIdAsync(KYCId);
        }

        public async Task<KycDocument> GetKycByUserIdAsync(Guid userId)
        {
            return await _kycRepository.GetKycByUserIdAsync(userId);
        }

        public async Task<KycDocument> AddKycDocumentAsync(KycDocumentDTO kycDocumentDto)
        {
            var kycDocument = _mapper.Map<KycDocument>(kycDocumentDto);

            kycDocument.UserImagePath = await ValidateAndUploadFile(kycDocument.UserImageFile);
            kycDocument.CitizenshipImagePath = await ValidateAndUploadFile(kycDocument.CitizenshipImageFile);

            if (kycDocument.UserImagePath != "" && kycDocument.CitizenshipImagePath != "") {
                kycDocument.IsApproved = true;
            }

            return await _kycRepository.AddKycDocumentAsync(kycDocument);
        }

        public async Task<KycDocument> UpdateKycDocumentAsync(Guid KYCId, KycDocumentDTO kycDocumentDto)
        {
            var kycDocument = _mapper.Map<KycDocument>(kycDocumentDto);
            return await _kycRepository.UpdateKycDocumentAsync(KYCId, kycDocument);
        }

        public void DeleteKycDocument(Guid KYCId)
        {
            _kycRepository.DeleteKycDocumentAsync(KYCId);
        }

        public async Task<KycDocument> UpdateKycDocumentAsync(Guid KYCId, JsonPatchDocument<KycDocumentDTO> kycDetails)
        {
            return await _kycRepository.UpdateKycDocumentAsync(KYCId, kycDetails);
        }

        public async Task<string> ValidateAndUploadFile(IFormFile fileInput)
        {
            var url = "";
            if (fileInput != null)
            {
                if (fileInput.Length > 1.5 * 1024 * 1024) // 1.5MB
                {
                    throw new CannotUnloadAppDomainException("File size exceeds the limit");
                }
                string fileExtension = Path.GetExtension(fileInput.FileName);
                if (fileExtension.ToLower() != ".png" && fileExtension.ToLower() != ".pdf")
                {
                    throw new CannotUnloadAppDomainException($"Invalid file type for {fileInput.FileName}");
                }
                else
                {
                    try
                    {
                        //storing image to firebase
                        var textInput = fileInput.FileName.Substring(fileInput.FileName.LastIndexOf("/") + 1);

                        // Copy the contents of the uploaded file to a memory stream
                        using var memoryStream = new MemoryStream();
                        await fileInput.CopyToAsync(memoryStream);
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        url = await _firebaseStorageHelper.UploadFileAsync(textInput, memoryStream); // pass the stream to the Firebase storage helper method
                    }
                    catch (Exception ex)
                    {
                        Trace.TraceError("An error occurred: {0}", ex.Message);
                    }
                }
            }
            return url;
        }
    }
}
