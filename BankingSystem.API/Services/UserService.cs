using BankingSystem.API.DTO;
using System.Text;
using System.Security.Cryptography;
using BankingSystem.API.IRepository;
using BankingSystem.API.Models;
using Microsoft.AspNetCore.JsonPatch;
using AutoMapper;

namespace BankingSystem.API.Services
{
    public class UserService
    {
        private readonly IUserRepository UserRepository;

        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            UserRepository = userRepository ?? throw new ArgumentOutOfRangeException(nameof(userRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<Users?> GetUserAsync(Guid userId)
        {
            //returns only user detail
            return await UserRepository.GetUserAsync(userId);
        }
        public async Task<Users?> GetUserByEmailAsync(string email)
        {
            //returns only user detail
            return await UserRepository.GetUserByEmailAsync(email);
        }

        public async Task<IEnumerable<Users>> GetUsersAsync()
        {
            return await UserRepository.GetUsersAsync();
        }

        public async Task<Users> AddUsers(UserDTO users)
        {
            var finalUser = _mapper.Map<Users>(users);

            // Hash password using bcrypt
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(users.Password);
            finalUser.Password = hashedPassword;

            return await UserRepository.AddUsers(finalUser);
        }

        public void DeleteUser(Guid userId)
        {
            UserRepository.DeleteUser(userId);
        }

        public async Task<Users> PatchUserDetails(Guid userId, JsonPatchDocument<UserDTO> patchDocument)
        {
            return await UserRepository.PatchUserDetails(userId, patchDocument);
        }

        public async Task<Users> UpdateUsersAsync(Guid userId, UserDTO users)
        {
            var finalUser = _mapper.Map<Users>(users);
            string hashedPassword = BCrypt.Net.BCrypt.HashPassword(users.Password);
            finalUser.Password = hashedPassword;

            return await UserRepository.UpdateUsersAsync(userId, finalUser);
        }

        public async Task<Users> LoginUser(string email, string password)
        {
            // Retrieve the hashed password for the given username/email from your user database
            var existingUser = await UserRepository.GetUserByEmailAsync(email);
            if (existingUser != null)
            {
                if (existingUser.Email.Equals(email) && BCrypt.Net.BCrypt.Verify(password, existingUser.Password))
                {
                    return existingUser;
                }
                else
                {
                    // Passwords don't match
                    return null;
                }
            }
            return null;
        }

        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
