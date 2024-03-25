using AutoMapper;
using BankingSystem.API.Data.Repository.IRepository;
using BankingSystem.API.Entities;
using BankingSystem.API.Services;
using BankingSystem.API.Utilities;
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

            var fileStorageHelperMock = new Mock<FileStorageHelper>(); // Mock FileStorageHelper if necessary for file upload operations

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

            var fileStorageHelperMock = new Mock<FileStorageHelper>();// Mock FileStorageHelper if necessary for file upload operations

            var kycService = new KycService(kycRepositoryMock.Object, mapperMock.Object, fileStorageHelperMock.Object);

            // Act
            var result = await kycService.GetKycDocumentAsync();

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<KycDocument>>(result);
            Assert.Equal(expectedKycDocuments.Count, ((List<KycDocument>)result).Count);
            // Additional assertions can be made based on the expected KycDocument instances
        }
    }
}
