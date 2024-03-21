﻿using Xunit;
using Moq;
using BankingSystem.API.Services;
using BankingSystem.API.Models;
using BankingSystem.API.IRepository;
using AutoMapper;
using System.Threading.Tasks;
using BankingSystem.API.DTO;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Net;


namespace BankingSystem.Test.UnitTests
{
    public class UserServiceTests
    {

        /*
         * Testing all returning data of a user with particuar user id
         */
        [Fact]
        public async Task GetUserAsync_ReturnsUser()
        {
            // Arrange
            var id = new Guid();
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(repo => repo.GetUserAsync(id))
                .ReturnsAsync(new Users { UserId = id, Username = "ishwor", Fullname = "Ishwor Shrestha", Address = "Pulchowk", Email = "ishwor@gmail.com" });

            var mapperMock = new Mock<IMapper>();
            var userService = new UserService(userRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await userService.GetUserAsync(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.UserId);
            Assert.Equal("Ishwor Shrestha", result.Fullname);
            Assert.Equal("ishwor", result.Username);
        }


        /*
         * Testing all returning data of all users
         */
        [Fact]
        public async Task GetUsersAsync_AllReturnsUsers()
        {
            // Arrange
            var id1 = new Guid();
            var id2 = new Guid();
            var userRepositoryMock = new Mock<IUserRepository>();
            var expectedUsers = new List<Users>
            {
                new Users { UserId = id1, Username = "ishwor", Fullname = "Ishwor Shrestha", Address = "Pulchowk", Email = "ishwor@gmail.com" },
                new Users { UserId = id2, Username = "ishwor2", Fullname = "Ishwor Shrestha 2", Address = "Pulchowk 2", Email = "ishwor2@gmail.com" }
            };
            userRepositoryMock.Setup(repo => repo.GetUsersAsync()).ReturnsAsync(expectedUsers);
            var mapperMock = new Mock<IMapper>();
            var userService = new UserService(userRepositoryMock.Object, mapperMock.Object);

            // Act
            var result = await userService.GetUsersAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedUsers, result);
        }


        //[Fact]
        //public async Task AddUsers_ValidUserDTO_ReturnsAddedUser()
        //{
        //    // Arrange
        //    var userRepositoryMock = new Mock<IUserRepository>();
        //    var mapperMock = new Mock<IMapper>();
        //    var userService = new UserService(userRepositoryMock.Object, mapperMock.Object);

        //    // Create a test user DTO
        //    var userDto = new UserDTO
        //    {
        //        Username = "usernameValue",
        //        Fullname = "fullnameValue",
        //        Email = "emailValue",
        //        Password = "passwordValue",
        //        Address = "addressValue",
        //        UserType = Roles.AccountHolder, // or whichever role you want
        //        DateOfBirth = DateTime.Parse("2000-01-01"), // example date of birth
        //        CreatedAt = DateTime.Now // or whichever creation date you want
        //    };

        //    // Define the expected user after mapping and hashing
        //    var expectedUser = new Users
        //    {
        //        UserId = 1,
        //        Username = "usernameValue",
        //        Fullname = "fullnameValue",
        //        Email = "emailValue",
        //        Password = "passwordValue",
        //        Address = "addressValue",
        //        UserType = Roles.AccountHolder, // or whichever role you want
        //        DateOfBirth = DateTime.Parse("2000-01-01"), // example date of birth
        //        CreatedAt = DateTime.Now // or whichever creation date you want
        //    };

        //    // Setup mapper to return expectedUser when mapping UserDTO
        //    mapperMock.Setup(mapper => mapper.Map<Users>(userDto)).Returns(expectedUser);

        //    // Setup repository to return expectedUser when adding user
        //    userRepositoryMock.Setup(repo => repo.AddUsers(expectedUser)).ReturnsAsync(expectedUser);

        //    // Act
        //    var result = await userService.AddUsers(userDto);

        //    // Assert
        //    Assert.NotNull(result);
        //    //Assert.Equal(expectedUser, result);
        //    Assert.Equal(expectedUser.UserId, result.UserId);
        //    Assert.Equal(expectedUser.Username, result.Username);
        //    Assert.Equal(expectedUser.Fullname, result.Fullname);
        //    Assert.Equal(expectedUser.Email, result.Email);
        //    Assert.Equal(expectedUser.Password, result.Password);
        //    Assert.Equal(expectedUser.Address, result.Address);
        //}

        // Similarly, write tests for other methods of UserService
    }
}