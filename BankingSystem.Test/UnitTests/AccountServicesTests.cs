using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AutoMapper;
using BankingSystem.API.Data.Repository.IRepository;
using BankingSystem.API.DTOs;
using BankingSystem.API.Entities;
using BankingSystem.API.Services;
using BankingSystem.API.Services.IServices;
using Moq;
using Xunit;

namespace BankingSystem.API.Tests
{
    public class AccountServicesTests
    {

        [Fact]
        public async Task AddAccountsValidInputReturnsAddedAccount()
        {
            // Arrange
            var mockAccountRepository = new Mock<IAccountRepository>();
            var mockEmailService = new Mock<IEmailService>();

            var mockMapper = new Mock<IMapper>();

            var accountDTO = new AccountDTO(
            id: Guid.NewGuid(),
            accountNumber: 1000010231785407,
            balance: 123456,
            atmCardNum: 9000099252964762,
            atmCardPin: 1234,
            accountCreatedAt: DateTime.UtcNow,
            accountCreatedBy: Guid.NewGuid(),
            accountModifiedAt: DateTime.UtcNow,
            accountModifiedBy: Guid.NewGuid()
              );


            var userCreationDTO = new UserCreationDTO
            (
               
                 username: "asdf",
              fullname: "hello world",
              email: "user@example.com",
             password: "hello",
             address: "earth",
             dateOfBirth: DateTime.UtcNow
                );

            var accounts = new Accounts
            {
                UserId = accountDTO.UserId,
                AccountId = Guid.NewGuid(), 
                AccountNumber = accountDTO.AccountNumber,
                AtmCardNum = accountDTO.AtmCardNum,
                AtmCardPin = accountDTO.AtmCardPin,
                CreatedBy =accountDTO.AccountCreatedBy,
                CreatedAt   =accountDTO.AccountCreatedAt,
                ModifiedAt = accountDTO.AccountModifiedAt,
                ModifiedBy = accountDTO.AccountModifiedBy,
            };

            mockMapper.Setup(m => m.Map<Accounts>(accountDTO)).Returns(accounts);
            mockAccountRepository.Setup(r => r.AddAccounts(accounts)).ReturnsAsync(accounts);

            var accountService = new AccountServices(mockAccountRepository.Object, mockEmailService.Object, mockMapper.Object);

            // Act
            var result = await accountService.AddAccounts(accountDTO, userCreationDTO.Email );

            // Assert
            Assert.Equal(accounts, result);
            mockAccountRepository.Verify(r => r.AddAccounts(accounts), Times.Once);
            mockEmailService.Verify(e => e.SendEmailAsync(It.IsAny<Email>()), Times.Once);
        }

        // Add more tests for other methods as needed
    }
}
