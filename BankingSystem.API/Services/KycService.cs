using AutoMapper;
using BankingSystem.API.Data.Repository.IRepository;
using BankingSystem.API.DTOs;
using BankingSystem.API.Entities;
using BankingSystem.API.Services.IServices;
using BankingSystem.API.Utilities;
using System.Diagnostics;

namespace BankingSystem.API.Services
{
    public class KycService : IKYCService
    {
        private readonly IKycRepository _kycRepository;
        private readonly IMapper _mapper;
        private readonly FileStorageHelper _fileStorageHelper;

        public KycService(IKycRepository kycRepository, IMapper mapper, IConfiguration configuration)
        {
            _kycRepository = kycRepository;
            _mapper = mapper;
            _fileStorageHelper = new FileStorageHelper(configuration);
        }

        public async Task<IEnumerable<KycDocument>> GetKycDocumentAsync()
        {
            return await _kycRepository.GetKycDocumentAsync();
        }

        public async Task<KycDocument?> GetKycDocumentAsync(Guid KYCId)
        {
            return await _kycRepository.GetKYCIdAsync(KYCId);
        }

        public async Task<KycDocument> GetKycByUserIdAsync(Guid Id)
        {
            return await _kycRepository.GetKycByUserIdAsync(Id);
        }

        public async Task<KycDocument> AddKycDocumentAsync(KycDocumentDTO kycDocumentDto)
        {
            var kycDocument = _mapper.Map<KycDocument>(kycDocumentDto);

            // Set UserImagePath to an empty string initially
            kycDocument.UserImagePath = "";

            // Upload files and set UserImagePath
            Console.WriteLine("Before uploading file - UserImagePath: " + kycDocument.UserImagePath); // Debugging statement
            kycDocument.UserImagePath = await ValidateAndUploadFile(kycDocumentDto.UserImageFile);
            Console.WriteLine("After uploading file - UserImagePath: " + kycDocument.UserImagePath); // Debugging statement
            kycDocument.CitizenshipImagePath = await ValidateAndUploadFile(kycDocumentDto.CitizenshipImageFile);

            if (kycDocument.UserImagePath != "" && kycDocument.CitizenshipImagePath != "")
            {
                kycDocument.IsApproved = true;
            }

            // Now you can add the KycDocument to the repository
            return await _kycRepository.AddKycDocumentAsync(kycDocument);
        }

        public async Task<KycDocument> UpdateKycDocumentAsync(Guid KYCId, KycDocumentDTO updatedKycDocumentDto)
        {
            var updatedKycDocument = _mapper.Map<KycDocument>(updatedKycDocumentDto);
            updatedKycDocument.UserImagePath = await ValidateAndUploadFile(updatedKycDocumentDto.UserImageFile);
            updatedKycDocument.CitizenshipImagePath = await ValidateAndUploadFile(updatedKycDocumentDto.CitizenshipImageFile);


            var existingKycDocument = await _kycRepository.GetKYCIdAsync(KYCId);
            if (existingKycDocument == null)
            {
                return null;
            }
            existingKycDocument.FatherName = updatedKycDocument.FatherName;
            existingKycDocument.MotherName = updatedKycDocument.MotherName;
            existingKycDocument.GrandFatherName = updatedKycDocument.GrandFatherName;
            existingKycDocument.PermanentAddress = updatedKycDocument.PermanentAddress;
            existingKycDocument.UploadedAt = updatedKycDocument.UploadedAt;
            existingKycDocument.UserImagePath = updatedKycDocument.UserImagePath;
            existingKycDocument.CitizenshipImagePath = updatedKycDocument.CitizenshipImagePath;

            if (existingKycDocument.UserImagePath != "" && updatedKycDocument.CitizenshipImagePath != "")
            {
                updatedKycDocument.IsApproved = true;
            }
            return await _kycRepository.UpdateKycDocumentAsync(KYCId, existingKycDocument);
        }

        public async Task<string> ValidateAndUploadFile(IFormFile fileInput)
        {
            if (fileInput == null)
            {
                throw new ArgumentNullException(nameof(fileInput));
            }
            var url = "";
            if (fileInput != null)
            {
                if (fileInput.Length > 1.5 * 1024 * 1024) // 1.5MB
                {
                    throw new CannotUnloadAppDomainException("File size exceeds the limit");
                }
                string fileExtension = Path.GetExtension(fileInput.FileName);
                if (fileExtension.ToLower() != ".png" && fileExtension.ToLower() != ".pdf" && fileExtension.ToLower() != ".jpeg")
                {
                    throw new CannotUnloadAppDomainException($"Invalid file type for {fileInput.FileName}");
                }
                else
                {
                    try
                    {
                        var textInput = fileInput.FileName.Substring(fileInput.FileName.LastIndexOf("/") + 1);

                        // Copy the contents of the uploaded file to a memory stream
                        using var memoryStream = new MemoryStream();
                        await fileInput.CopyToAsync(memoryStream);
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        url = await _fileStorageHelper.UploadFileAsync(textInput, memoryStream); // pass the stream to the Firebase storage helper method
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
