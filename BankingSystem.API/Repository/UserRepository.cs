using BankingSystem.API.DTO;
using BankingSystem.API.IRepository;
using BankingSystem.API.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.EntityFrameworkCore;

namespace RESTful_API__ASP.NET_Core.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentOutOfRangeException(nameof(context));
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

        public async Task<Users> PatchUserDetails(Guid Id, JsonPatchDocument<UserDTO> patchDocument)
        {
            var existingUser = await GetUserAsync(Id);
            if (existingUser != null)
            {
                //transform user entity to usercreationDTO
                var userToPatch = new UserDTO(existingUser.UserName, existingUser.Fullname, existingUser.Email, existingUser.PasswordHash, existingUser.Address, /*existingUser.UserType,*/ existingUser.DateOfBirth);

                patchDocument.ApplyTo(userToPatch);

                existingUser.UserName = userToPatch.Username;
                existingUser.Fullname = userToPatch.Fullname;
                existingUser.Email = userToPatch.Email;

                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(userToPatch.Password);

                existingUser.PasswordHash = hashedPassword;
                existingUser.Address = userToPatch.Address;
                //existingUser.UserType = userToPatch.UserType;
                existingUser.DateOfBirth = userToPatch.DateOfBirth;
                
                //update modifiedAt DateTime
                existingUser.ModifiedAt = DateTime.UtcNow;

                _context.SaveChanges();
                return existingUser;
            }
            return null;
        }

        public async Task<Users> UpdateUsersAsync(Guid Id, Users finalUser)
        {
            var existingUser = await GetUserAsync(Id);
            if (existingUser != null)
            {
                existingUser.UserName = finalUser.UserName;
                existingUser.Fullname = finalUser.Fullname;
                existingUser.Email = finalUser.Email;
                existingUser.PasswordHash = finalUser.PasswordHash;
                existingUser.Address = finalUser.Address;
                //existingUser.UserType = finalUser.UserType;
                existingUser.DateOfBirth = finalUser.DateOfBirth;

                //update modifiedAt DateTime
                existingUser.ModifiedAt = DateTime.UtcNow;

                _context.SaveChanges();
                return existingUser;
            }
            return null;
        }
    }
}