using BankingSystem.API.DTO;
using BankingSystem.API.IRepository;
using BankingSystem.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using RESTful_API__ASP.NET_Core.Repository;
using BankingSystem.API.Utils;

namespace BankingSystem.API.Services
{
    public class KycService
    {
        private readonly IKycRepository _kycRepository;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        private readonly FirebaseStorageHelper _firebaseStorageHelper;

        public KycService(IKycRepository kycRepository, IUserRepository userRepository, IMapper mapper, FirebaseStorageHelper firebaseStorageHelper)
        {
            _kycRepository = kycRepository;
            _userRepository = userRepository ;
            _mapper = mapper;
            _firebaseStorageHelper = firebaseStorageHelper;
        }

        public async Task<KycDocument> AddKycDocumentAsync(KycDocumentDTO kycDocumentDto, IFormFile userImageFile, IFormFile citizenshipImageFile)
        {
            string userImagePath = await UploadFileToFirebaseStorage(userImageFile);
            string citizenshipImagePath = await UploadFileToFirebaseStorage(citizenshipImageFile);
            var kycDocument = _mapper.Map<KycDocument>(kycDocumentDto);
            kycDocument.UserImagePath = userImagePath;
            kycDocument.CitizenshipImagePath = citizenshipImagePath;
            var addedKycDocument = await _kycRepository.AddKycDocumentAsync(kycDocument);
            return addedKycDocument; 
        }
        private async Task<string> UploadFileToFirebaseStorage(IFormFile file)
        {
            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await file.CopyToAsync(memoryStream);
                    memoryStream.Position = 0;
                    return await _firebaseStorageHelper.UploadFileAsync(Guid.NewGuid().ToString(), memoryStream);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to upload file to Firebase Storage", ex);
            }
        }

        public async Task<IEnumerable<KycDocument>> GetKycDocumentAsync()
        {
            return await _kycRepository.GetKycDocumentAsync();
        }

        public async Task<KycDocument?> GetKycDocumentAsync(int KYCId)
        {
            return await _kycRepository.GetKYCIdAsync(KYCId);
        }

        public async Task<KycDocument> GetKycByUserIdAsync(int userId)
        {
            return await _kycRepository.GetKycByUserIdAsync(userId);
        }

        public async Task<KycDocument> AddKycDocumentAsync(KycDocumentDTO kycDocumentDto)
        {
            var kycDocument = _mapper.Map<KycDocument>(kycDocumentDto);
            return await _kycRepository.AddKycDocumentAsync(kycDocument);
        }

        public async Task<KycDocument> UpdateKycDocumentAsync(int KYCId, KycDocumentDTO kycDocumentDto)
        {
            var kycDocument = _mapper.Map<KycDocument>(kycDocumentDto);
            return await _kycRepository.UpdateKycDocumentAsync(KYCId, kycDocument);
        }

        public void DeleteKycDocument(int KYCId)
        {
            _kycRepository.DeleteKycDocumentAsync(KYCId);
        }

        public async Task<KycDocument> UpdateKycDocumentAsync(int KYCId, JsonPatchDocument<KycDocumentDTO> kycDetails)
        {
            return await _kycRepository.UpdateKycDocumentAsync(KYCId, kycDetails);
        }
    
    }
}
