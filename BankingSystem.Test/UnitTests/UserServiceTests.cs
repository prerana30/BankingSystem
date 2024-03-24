using AutoMapper;
using BankingSystem.API.Data.Repository.IRepository;
using BankingSystem.API.DTO;
using BankingSystem.API.Models;
using BankingSystem.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;
using System.Globalization;

namespace BankingSystem.Test.UnitTests
{
    public class UserServiceTests
    {
        [Fact]
        public async Task RegisterUser_WithValidData_ShouldReturnUserInfoDisplayDTO()
        {
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UserCreationDTO, Users>();
                cfg.CreateMap<Users, UserInfoDisplayDTO>();
            });
            var mapper = mapperConfig.CreateMapper();

            var mapperConfig1 = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountDTO, Accounts>();
            });
            var mapper1 = mapperConfig1.CreateMapper();

            var accountRepositoryMock = new Mock<IAccountRepository>();
            var configurationMock = new Mock<IConfiguration>();
            var userManagerMock = MockUserManager<Users>();
            var signInManagerMock = MockSignInManager<Users>();
            var passwordHasherMock = new Mock<IPasswordHasher<Users>>();

            var emailServiceMock = new Mock<EmailService>(configurationMock.Object);
            // EmailService mock setup
            emailServiceMock.Setup(es => es.SendEmailAsync(It.IsAny<Email>())).Returns(Task.CompletedTask); // Mock the SendEmailAsync method

            var accountServicesMock = new Mock<AccountServices>(accountRepositoryMock.Object, emailServiceMock.Object, mapper1);

            var userService = new UserService(userRepositoryMock.Object, mapper, accountServicesMock.Object, userManagerMock.Object, signInManagerMock.Object, passwordHasherMock.Object);

            var userCreationDTO = new UserCreationDTO
            {
                UserName = "ishwor",
                Fullname = "Ishwor Shrestha",
                Address = "Pulchowk",
                Email = "ishwor@gmail.com",
                Password = "password", // Assuming you also set the password in the DTO
                PhoneNumber = "string",
                UserType = 0,
                DateOfBirth = DateTime.SpecifyKind(DateTime.ParseExact("2000-03-23T11:13:25.342Z", "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture), DateTimeKind.Utc)
            };

            userRepositoryMock.Setup(repo => repo.GetUserByEmailAsync(It.IsAny<string>())).ReturnsAsync((Users)null);
            userRepositoryMock.Setup(repo => repo.AddUsers(It.IsAny<Users>())).ReturnsAsync(new Users());

            userManagerMock.Setup(um => um.FindByEmailAsync(It.IsAny<string>())).ReturnsAsync((Users)null);
            userManagerMock.Setup(um => um.FindByNameAsync(It.IsAny<string>())).ReturnsAsync((Users)null);
            userManagerMock.Setup(um => um.CreateAsync(It.IsAny<Users>(), It.IsAny<string>())).ReturnsAsync(IdentityResult.Success);
            userManagerMock.Setup(um => um.GetRolesAsync(It.IsAny<Users>())).ReturnsAsync(new List<string>());

            signInManagerMock.Setup(sm => sm.PasswordSignInAsync(It.IsAny<string>(), It.IsAny<string>(), true, false))
                .ReturnsAsync(SignInResult.Success);

            passwordHasherMock.Setup(ph => ph.HashPassword(It.IsAny<Users>(), It.IsAny<string>()))
                .Returns((Users user, string password) => password); // Mock password hashing

            // Act
            var result = await userService.RegisterUser(userCreationDTO);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Ishwor Shrestha", result.Fullname);
            Assert.Equal("ishwor", result.UserName);
        }

        // Helper method to mock UserManager
        public static Mock<UserManager<TUser>> MockUserManager<TUser>() where TUser : class
        {
            var userStoreMock = new Mock<IUserStore<TUser>>();
            return new Mock<UserManager<TUser>>(userStoreMock.Object, null, null, null, null, null, null, null, null);
        }

        // Helper method to mock SignInManager
        public static Mock<SignInManager<TUser>> MockSignInManager<TUser>() where TUser : class
        {
            var userStoreMock = new Mock<IUserStore<TUser>>();
            var userManagerMock = MockUserManager<TUser>();
            return new Mock<SignInManager<TUser>>(userManagerMock.Object, new HttpContextAccessor(), new Mock<IUserClaimsPrincipalFactory<TUser>>().Object, null, null, null, null);
        }


        [Fact]
        public async Task GetUserAsync_ReturnsUser()
        {
            // Arrange
            var Id = new Guid();
            // Arrange
            var userRepositoryMock = new Mock<IUserRepository>();
            
            var mapperMock = new Mock<IMapper>();

            var mapperConfig1 = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountDTO, Accounts>();
            });
            var mapper1 = mapperConfig1.CreateMapper();

            var accountRepositoryMock = new Mock<IAccountRepository>();
            var configurationMock = new Mock<IConfiguration>();
            var userManagerMock = MockUserManager<Users>();
            var signInManagerMock = MockSignInManager<Users>();
            var passwordHasherMock = new Mock<IPasswordHasher<Users>>();

            var emailServiceMock = new Mock<EmailService>(configurationMock.Object);
            emailServiceMock.Setup(es => es.SendEmailAsync(It.IsAny<Email>())).Returns(Task.CompletedTask); // Mock the SendEmailAsync method

            var accountServicesMock = new Mock<AccountServices>(accountRepositoryMock.Object, emailServiceMock.Object, mapper1);

            var userService = new UserService(userRepositoryMock.Object, mapperMock.Object, accountServicesMock.Object, userManagerMock.Object, signInManagerMock.Object, passwordHasherMock.Object);

            // Set up mocks
            userRepositoryMock.Setup(repo => repo.GetUserAsync(Id))
                .ReturnsAsync(new Users { Id = Id, UserName = "ishwor", Fullname = "Ishwor Shrestha", Address = "Pulchowk", Email = "ishwor@gmail.com" });

            // Act
            var result = await userService.GetUserAsync(Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(Id, result.Id);
            Assert.Equal("Ishwor Shrestha", result.Fullname);
            Assert.Equal("ishwor", result.UserName);
        }

        [Fact]
        public async Task GetUsersAsync_AllReturnsUsers()
        {
            // Arrange
            var id1 = new Guid();
            var id2 = new Guid();

            var userRepositoryMock = new Mock<IUserRepository>();
            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Users, UserInfoDisplayDTO>();
            });
            var mapper = mapperConfig.CreateMapper();

            var mapperConfig1 = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<AccountDTO, Accounts>();
            });
            var mapper1 = mapperConfig1.CreateMapper();

            var accountRepositoryMock = new Mock<IAccountRepository>();
            var configurationMock = new Mock<IConfiguration>();
            var userManagerMock = MockUserManager<Users>();

            var signInManagerMock = MockSignInManager<Users>();
            var passwordHasherMock = new Mock<IPasswordHasher<Users>>();

            var emailServiceMock = new Mock<EmailService>(configurationMock.Object);
            emailServiceMock.Setup(es => es.SendEmailAsync(It.IsAny<Email>())).Returns(Task.CompletedTask); // Mock the SendEmailAsync method

            var accountServicesMock = new Mock<AccountServices>(accountRepositoryMock.Object, emailServiceMock.Object, mapper1);

            var userService = new UserService(userRepositoryMock.Object, mapper, accountServicesMock.Object, userManagerMock.Object, signInManagerMock.Object, passwordHasherMock.Object);

            // Set up mocks
            var expectedUsers = new List<Users>
            {
                new Users {
                    Id= id1,
                    UserName = "ishwor",
                    Fullname = "Ishwor Shrestha",
                    Address = "Pulchowk",
                    Email = "ishwor@gmail.com",
                    PasswordHash = "password", // Assuming you also set the password in the DTO
                    PhoneNumber = "string",
                    DateOfBirth = DateTime.SpecifyKind(DateTime.ParseExact("2000-03-23T11:13:25.342Z", "yyyy-MM-ddTHH:mm:ss.fffZ", CultureInfo.InvariantCulture), DateTimeKind.Utc)},
                new Users {
                    Id = id2, UserName = "ishwor2", Fullname = "Ishwor Shrestha 2", Address = "Pulchowk 2", Email = "ishwor2@gmail.com" }
            };

            // Configure userManagerMock to return expectedUsers when accessed
            userManagerMock.Setup(um => um.Users).Returns(expectedUsers.AsQueryable());
            // Mock GetRolesAsync method
            userManagerMock.Setup(um => um.GetRolesAsync(It.IsAny<Users>())).ReturnsAsync(new List<string> { "AccountHolder", "TellerPerson" });

            // Act
            var users = await userService.GetUsersAsync(); // Retrieve the task

            // Assert
            Assert.NotNull(users);
            var userList = users.ToList(); // Convert the IEnumerable to a List
            Assert.Equal(expectedUsers.Count, userList.Count);
            Assert.Equal(expectedUsers.First().Id, userList.First().Id);
            Assert.Equal(expectedUsers.First().Fullname, userList.First().Fullname);
            Assert.Equal(expectedUsers.Last().Id, userList.Last().Id);
            Assert.Equal(expectedUsers.Last().Fullname, userList.Last().Fullname);
        }
    }
}
