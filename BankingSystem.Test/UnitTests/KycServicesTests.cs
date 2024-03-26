using AutoMapper;
using BankingSystem.API.Data.Repository.IRepository;
using BankingSystem.API.DTOs;
using BankingSystem.API.Entities;
using BankingSystem.API.Services;
using BankingSystem.API.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Moq;

namespace BankingSystem.Test.UnitTests
{
    public class KycServicesTests
    {
        [Fact]
        public async Task GetKycByUserIdAsync_ReturnsKycDocument()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var expectedKycDocument = new KycDocument
            {
                UserId = userId,
            };

            var kycRepositoryMock = new Mock<IKycRepository>();
            kycRepositoryMock.Setup(repo => repo.GetKycByUserIdAsync(userId))
                .ReturnsAsync(expectedKycDocument);

            var mapperMock = new Mock<IMapper>(); // Mock IMapper if necessary for mapping operations

            var fileStorageHelperMock = new Mock<IConfiguration>(); // Mock FileStorageHelper if necessary for file upload operations

            var kycService = new KycService(kycRepositoryMock.Object, mapperMock.Object, fileStorageHelperMock.Object);

            // Act
            var result = await kycService.GetKycByUserIdAsync(userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<KycDocument>(result);
            Assert.Equal(expectedKycDocument.UserId, result.UserId);
            // Additional assertions can be made based on other properties of the KycDocument object
        }


        [Fact]
        public async Task GetKycDocumentAsync_ReturnsKycDocuments()
        {
            // Arrange
            var expectedKycDocuments = new List<KycDocument>
            {
                new KycDocument { UserId = Guid.NewGuid(), /* Other properties */ },
                new KycDocument { UserId = Guid.NewGuid(), /* Other properties */ },
                // Add more KycDocument instances as needed
            };

            var kycRepositoryMock = new Mock<IKycRepository>();
            kycRepositoryMock.Setup(repo => repo.GetKycDocumentAsync())
                .ReturnsAsync(expectedKycDocuments);

            var mapperMock = new Mock<IMapper>();// Mock IMapper if necessary for mapping operations

            var fileStorageHelperMock = new Mock<IConfiguration>();// Mock FileStorageHelper if necessary for file upload operations

            var kycService = new KycService(kycRepositoryMock.Object, mapperMock.Object, fileStorageHelperMock.Object);

            // Act
            var result = await kycService.GetKycDocumentAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<KycDocument>>(result);
            Assert.Equal(expectedKycDocuments.Count, ((List<KycDocument>)result).Count);
            // Additional assertions can be made based on the expected KycDocument instances
        }

        //Test for Empty User Id:

        [Fact]
        public async Task GetKycByUserIdAsync_EmptyUserId_ReturnsNull()
        {
            // Arrange
            var userId = Guid.Empty; // Empty user ID
            KycDocument expectedKycDocument = null; // We expect null as the result for an empty user ID

            var kycRepositoryMock = new Mock<IKycRepository>();
            kycRepositoryMock.Setup(repo => repo.GetKycByUserIdAsync(userId))
                .ReturnsAsync(expectedKycDocument!);

            var mapperMock = new Mock<IMapper>(); // Mock IMapper if necessary for mapping operations

            var fileStorageHelperMock = new Mock<IConfiguration>(); // Mock FileStorageHelper if necessary for file upload operations

            var kycService = new KycService(kycRepositoryMock.Object, mapperMock.Object, fileStorageHelperMock.Object);

            // Act
            var result = await kycService.GetKycByUserIdAsync(userId);

            // Assert
            Assert.Null(result); // Ensure that the result is null for an empty user ID
        }

        // Test for multiple concurrent requests
        [Fact]
        public async Task GetKycByUserIdAsync_ConcurrencyTest()
        {
            // Arrange
            var userId = Guid.NewGuid(); // Generate a valid user ID
            var kycRepository = new Mock<IKycRepository>(); // Use a mock repository for testing concurrency
            var mapper = new Mock<IMapper>(); // Use mock mapper
            var fileStorageHelper = new Mock<IConfiguration>(); // Use mock file storage helper
            var kycService = new KycService(kycRepository.Object, mapper.Object, fileStorageHelper.Object);

            // Simulate concurrent requests
            var tasks = new List<Task<KycDocument>>();
            for (int i = 0; i < 10; i++)
            {
                tasks.Add(Task.Run(async () => await kycService.GetKycByUserIdAsync(userId)));
            }

            // Act
            await Task.WhenAll(tasks);

            // Assert
            // Add assertions to verify the behavior under concurrent scenarios
        }

        //-------------------Post Test--------------//

        [Fact]
        public async Task ValidateAndUploadFile_WithNullInput_ShouldThrowArgumentNullException()
        {
            // Arrange
            var kycService = new KycService(
                Mock.Of<IKycRepository>(), // Mocked repository
                Mock.Of<IMapper>(), // Mocked mapper
                Mock.Of<IConfiguration>() // Mocked file storage helper
            );

            // Act and Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => kycService.ValidateAndUploadFile(null));
        }

        // AddKycDocumentAsync test
        [Fact]
        public async Task AddKycDocumentAsync_WithValidData_ShouldReturnAddedKycDocument()
        {
            // Arrange
            var kycDocumentDto = new KycDocumentDTO
            {
                UserId = Guid.NewGuid(),
                FatherName = "John",
                MotherName = "Doe",
                GrandFatherName = "Smith",
                UserImageFile = new FormFile(null, 0, 0, "userImage", "userImage.jpeg"),
                CitizenshipImageFile = new FormFile(null, 0, 0, "citizenshipImage", "citizenshipImage.jpeg"),
                PermanentAddress = "123 Main St"
            };

            var kycDocument = new KycDocument
            {
                UserId = kycDocumentDto.UserId,
                FatherName = kycDocumentDto.FatherName,
                MotherName = kycDocumentDto.MotherName,
                GrandFatherName = kycDocumentDto.GrandFatherName,
                PermanentAddress = kycDocumentDto.PermanentAddress,
                // Mocking other properties as needed
            };

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<KycDocument>(It.IsAny<KycDocumentDTO>()))
                .Returns(kycDocument);

            var kycRepositoryMock = new Mock<IKycRepository>();
            kycRepositoryMock.Setup(repo => repo.AddKycDocumentAsync(It.IsAny<KycDocument>()))
                .ReturnsAsync(kycDocument);

            var kycService = new KycService(kycRepositoryMock.Object, mapperMock.Object, null);

            // Act
            var result = await kycService.AddKycDocumentAsync(kycDocumentDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(kycDocument.UserId, result.UserId);
            Assert.Equal(kycDocument.FatherName, result.FatherName);
            Assert.Equal(kycDocument.MotherName, result.MotherName);
            // Assert other properties as needed
        }
    }
}