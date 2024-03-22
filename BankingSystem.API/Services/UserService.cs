using AutoMapper;
using BankingSystem.API.DTO;
using BankingSystem.API.IRepository;
using BankingSystem.API.Models;
using BankingSystem.API.Utils;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace BankingSystem.API.Services
{
    public class UserService
    {
        private readonly IUserRepository UserRepository;
        private readonly AccountServices AccountServices;

        private readonly IMapper _mapper;

        private readonly UserManager<Users> _userManager;
        private readonly SignInManager<Users> _signInManager;

        public UserService(IUserRepository userRepository, IMapper mapper, AccountServices accountServices, UserManager<Users> userManager, SignInManager<Users> signInManager)
        {
            UserRepository = userRepository ?? throw new ArgumentOutOfRangeException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            AccountServices = accountServices;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<Users?> GetUserAsync(Guid Id)
        {
            //returns only user detail
            return await UserRepository.GetUserAsync(Id);
        }
        public async Task<Users?> GetUserByEmailAsync(string email)
        {
            //returns only user detail
            return await _userManager.FindByEmailAsync(email);
        }

        public async Task<IEnumerable<Users>> GetUsersAsync()
        {
            return await _userManager.Users.ToListAsync();
        }

        public async Task<Users> RegisterUser(UserDTO users)
        {
            var finalUser = _mapper.Map<Users>(users);

            finalUser.PasswordHash = users.Password;

            var emailDuplication = _userManager.FindByEmailAsync(users.Email);
            if (emailDuplication.Result != null)
            {
                throw new Exception("Duplicate Email Address!");
            }

            var usernameDuplication = _userManager.FindByNameAsync(users.Username);
            if (usernameDuplication.Result != null)
            {
                throw new Exception("Duplicate UserName!");
            }

            var user = new Users()
            {
                UserName = finalUser.UserName,
                Fullname = finalUser.Fullname,
                Email = finalUser.Email,
                PhoneNumber = finalUser.PhoneNumber,
                Address = finalUser.Address,
                DateOfBirth = finalUser.DateOfBirth,
                CreatedAt = DateTime.UtcNow,
                ModifiedAt = DateTime.UtcNow,

                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                TwoFactorEnabled = false,
                LockoutEnabled = false,
                AccessFailedCount = 0,
            };
            try
            {
                var result = await _userManager.CreateAsync(user, finalUser.PasswordHash);
                if (result.Errors.Any())
                {
                    var description = result.Errors.Select(x => x.Description).First();
                    throw new Exception(description);
                }

                await _userManager.AddToRoleAsync(user, users.UserType);

                if (users.UserType == UserRoles.AccountHolder)
                {
                    var accountNumber = RandomNumberGeneratorHelper.GenerateRandomNumber(1);
                    var atmCardNum = RandomNumberGeneratorHelper.GenerateRandomNumber(2);
                    var atmCardPin = (int)RandomNumberGeneratorHelper.GenerateRandomNumber(3);

                    var accountDTO = new AccountDTO(user.Id, accountNumber, 0, atmCardNum, atmCardPin, DateTime.UtcNow, user.Id, DateTime.UtcNow, user.Id);

                    var checkAccount = await AccountServices.GetAccountByUserIdAsync(user.Id);
                    if (checkAccount != null)
                    {
                        throw new Exception("User already has an account.");
                    }
                    await AccountServices.AddAccounts(accountDTO);
                }
                return user;
            }
            catch (Exception e)
            {
                var errorMsg = $"Could not create user!, {e}";
                Console.WriteLine(errorMsg);
                throw new Exception(errorMsg);
            }
        }

        public void DeleteUser(Guid Id)
        {
            UserRepository.DeleteUser(Id);
        }

        public async Task<Users> PatchUserDetails(Guid Id, JsonPatchDocument<UserDTO> patchDocument)
        {
            return await UserRepository.PatchUserDetails(Id, patchDocument);
        }

        public async Task<Users> UpdateUsersAsync(Guid Id, UserDTO users)
        {
            var finalUser = _mapper.Map<Users>(users);
            finalUser.PasswordHash = users.Password;

            return await UserRepository.UpdateUsersAsync(Id, finalUser);
        }

        public async Task<Users> Login(string username, string password)
        {
            try
            {
                var result = await _signInManager.PasswordSignInAsync(username, password, true, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    // User is successfully logged in, retrieve the user from the database
                    var existingUser = await _userManager.FindByNameAsync(username);
                    return existingUser;
                }
                else if (result.IsLockedOut)
                {
                    // Handle locked-out user
                    throw new Exception("User account is locked out.");
                }
                else
                {
                    // Handle failed login attempt
                    throw new Exception("Invalid login attempt.");
                }
            }
            catch (Exception e)
            {
                // Log and re-throw the exception
                Console.WriteLine($"Error occurred during user login: {e}");
                throw;
            }
        }
    }
}
