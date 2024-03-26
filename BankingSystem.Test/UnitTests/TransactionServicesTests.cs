using AutoMapper;
using BankingSystem.API.Data.Repository.IRepository;
using BankingSystem.API.DTOs;
using BankingSystem.API.Entities;
using BankingSystem.API.Services;
using Moq;

namespace BankingSystem.Test.UnitTests
{
    public class TransactionServicesTests
    {
        [Fact]
        public async Task GetTransactionsOfAccountAsync_ReturnsTransactions()
        {
            // Arrange
            var accountId = Guid.NewGuid();
            var expectedTransactions = new List<Transaction>
            {
                new Transaction { AccountId = Guid.NewGuid(), Amount = 100, TransactionTime = DateTime.Now },
                new Transaction { AccountId = Guid.NewGuid(), Amount = 200, TransactionTime = DateTime.Now.AddDays(-1) }
            };

            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            transactionRepositoryMock.Setup(repo => repo.GetTransactionsOfAccountAsync(accountId))
                .ReturnsAsync(expectedTransactions);

            var mapperMock = new Mock<IMapper>();

            var transactionServices = new TransactionServices(transactionRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await transactionServices.GetTransactionsOfAccountAsync(accountId);

            // Assert
            Assert.NotNull(result);
            Assert.IsAssignableFrom<IEnumerable<Transaction>>(result);
            Assert.Equal(expectedTransactions.Count, ((List<Transaction>)result).Count);
        }



        [Fact]
        public async Task DepositTransactionAsync_ReturnsTransaction()
        {
            // Arrange
            var accountNumber = 112233445566;
            var userId = Guid.NewGuid();
            var depositTransactionDto = new DepositTransactionDTO { Amount = 100 }; // Example DTO for deposit transaction

            var expectedTransaction = new Transaction { AccountId = Guid.NewGuid(), Amount = 100, TransactionTime = DateTime.Now }; // Example expected transaction

            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            transactionRepositoryMock.Setup(repo => repo.TellerDepositTransactionAsync(It.IsAny<Transaction>(), accountNumber, userId))
                .ReturnsAsync(expectedTransaction);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Transaction>(depositTransactionDto)).Returns(new Transaction()); // Mock mapper to return a new Transaction instance

            var transactionServices = new TransactionServices(transactionRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await transactionServices.TellerDepositTransactionAsync(depositTransactionDto, accountNumber, userId);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Transaction>(result);
            Assert.Equal(expectedTransaction.Amount, result.Amount);
        }



        [Fact]
        public async Task WithdrawTransactionAsync_ReturnsTransaction()
        {
            // Arrange
            var accountNumber = 112233445566;
            var atmIdAtmCardPin = 1234; // Example ATM ID or ATM card PIN
            var withdrawTransactionDto = new WithdrawTransactionDTO { Amount = 50 }; // Example DTO for withdraw transaction

            var expectedTransaction = new Transaction { Amount = 50, TransactionTime = DateTime.Now }; // Example expected transaction

            var transactionRepositoryMock = new Mock<ITransactionRepository>();
            transactionRepositoryMock.Setup(repo => repo.SelfWithdrawTransactionAsync(It.IsAny<Transaction>(), accountNumber, atmIdAtmCardPin))
                .ReturnsAsync(expectedTransaction);

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(mapper => mapper.Map<Transaction>(withdrawTransactionDto)).Returns(new Transaction()); // Mock mapper to return a new Transaction instance

            var transactionServices = new TransactionServices(transactionRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await transactionServices.SelfWithdrawTransactionAsync(withdrawTransactionDto, accountNumber, atmIdAtmCardPin);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<Transaction>(result);
            Assert.Equal(expectedTransaction.Amount, result.Amount);
        }
    }
}


