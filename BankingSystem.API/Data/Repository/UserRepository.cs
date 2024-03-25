using BankingSystem.API.Data.Repository.IRepository;
using BankingSystem.API.DTO;
using BankingSystem.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace BankingSystem.API.Data.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<Users> _passwordHasher;

        public UserRepository(ApplicationDbContext context, IPasswordHasher<Users> passwordHasher)
        {
            _context = context ?? throw new ArgumentOutOfRangeException(nameof(context));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }
        public async Task<Users?> GetUserAsync(Guid Id)
        {
            //returns only user detail
            return await _context.SystemUser.Where(u => u.Id == Id).FirstOrDefaultAsync();
        }
        public async Task<Users?> GetUserByEmailAsync(string email)
        {
            //returns only user detail
            return await _context.SystemUser.Where(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Users>> GetUsersAsync()
        {
            return await _context.SystemUser.OrderBy(c => c.Fullname).ToListAsync();
        }

        public async Task<Users> AddUsers(Users users)
        {
            var user = _context.SystemUser.Add(users);
            await _context.SaveChangesAsync();

            return GetUserAsync(user.Entity.Id).Result;
        }

        public void DeleteUser(Guid userId)
        {
            var user = GetUserAsync(userId);
            _context.SystemUser.Remove(user.Result);
            _context.SaveChangesAsync();
        }

        public async Task<Users> PatchUserDetails(Guid Id, JsonPatchDocument<UserCreationDTO> patchDocument)
        {
            var existingUser = await GetUserAsync(Id);
            if (existingUser != null)
            {
                //transform user entity to usercreationDTO
                var userToPatch = new UserCreationDTO(existingUser.UserName, existingUser.Fullname, existingUser.Email, existingUser.PasswordHash, existingUser.Address, existingUser.DateOfBirth);

                patchDocument.ApplyTo(userToPatch);

                existingUser.UserName = userToPatch.UserName;
                existingUser.Fullname = userToPatch.Fullname;
                existingUser.Email = userToPatch.Email;

                string hashedPassword = _passwordHasher.HashPassword(existingUser, userToPatch.Password);

                existingUser.PasswordHash = hashedPassword;
                existingUser.Address = userToPatch.Address;
                existingUser.DateOfBirth = userToPatch.DateOfBirth;

                //update modifiedAt DateTime
                existingUser.ModifiedAt = DateTime.UtcNow;

                _context.SaveChangesAsync();
                return existingUser;
            }
            return null;
        }

        public async Task<Users> UpdateUsersAsync(Guid Id, Users finalUser)
        {
            var existingUser = await GetUserAsync(Id);
            if (existingUser != null)
            {
                // Check and update only the fields that are sent from the UI
                if (!string.IsNullOrEmpty(finalUser.Fullname) && existingUser.Fullname != finalUser.Fullname)
                    existingUser.Fullname = finalUser.Fullname;

                if (!string.IsNullOrEmpty(finalUser.UserName) && existingUser.UserName != finalUser.UserName)
                    existingUser.UserName = finalUser.UserName;

                if (!string.IsNullOrEmpty(finalUser.Email) && existingUser.Email != finalUser.Email)
                    existingUser.Email = finalUser.Email;

                if (finalUser.DateOfBirth != new DateTime() && existingUser.DateOfBirth != finalUser.DateOfBirth)
                    existingUser.DateOfBirth = finalUser.DateOfBirth;

                if (!string.IsNullOrEmpty(finalUser.PasswordHash) && existingUser.PasswordHash != finalUser.PasswordHash)
                    existingUser.PasswordHash = finalUser.PasswordHash;

                if (!string.IsNullOrEmpty(finalUser.Address) && existingUser.Address != finalUser.Address)
                    existingUser.Address = finalUser.Address;

                //update modifiedAt DateTime
                existingUser.ModifiedAt = DateTime.UtcNow;

                _context.SaveChangesAsync();
                return existingUser;
            }
            return null;
        }
    }
}